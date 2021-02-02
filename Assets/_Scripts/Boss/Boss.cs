using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class Boss : MonoBehaviour, IHitable
{
	[SerializeField] private Color            gizmosColor;
	[SerializeField] public  List<BossAction> actions;

	public int maxHealth = 1000;
	public int health    = 1000;

	public void GetHit(int damage)
	{
		health -= damage;

		if (health <= 0)
			Die();
	}

	public void Die()
	{
		UiController.Instance.bossSlider.gameObject.SetActive(false);
		StopAllCoroutines();
		StopActions();
		Destroy(gameObject);
	}

	public void Start()
	{
		health = maxHealth;

		UiController.Instance.bossSlider.maxValue = maxHealth;
		UiController.Instance.bossSlider.gameObject.SetActive(true);

		StartCoroutine(DoActions());
	}

	public void Update()
	{
		UiController.Instance.bossSlider.value = health;
	}

	private IEnumerator DoActions()
	{
		foreach (var bossAction in actions)
		{
			Debug.Log($"Running action {bossAction.GetType()}", bossAction);
			if (bossAction.waitForEnd)
				yield return bossAction.DoAction();
			else
				StartCoroutine(bossAction.DoAction());
		}
	}

	private void StopActions()
	{
		foreach (var bossAction in actions)
		{
			bossAction.StopAllCoroutines();
		}
	}

	# if UNITY_EDITOR
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = gizmosColor;

		var points = GetMovePoints(actions);
		for (var index = 0; index < points.Count; index++)
		{
			var point = points[index];
			Gizmos.DrawSphere(point, .5f);
			var labelPos = point + .5f * Vector3.down;
			Handles.Label(labelPos, $"{index + 1}");
		}
	}
	#endif
	
	private static List<Vector3> GetMovePoints(IEnumerable<BossAction> actions)
	{
		var pointList = new List<Vector3>();
		foreach (var action in actions)
		{
			if (action is null)
				continue;
			
			if (action.GetType() == typeof(MoveToPoint))
				pointList.Add(((MoveToPoint) action).point);
			else if (action.GetType() == typeof(Loop))
				pointList.AddRange(GetMovePoints(((Loop) action).actions));
		}

		return pointList;
	}
}

# if UNITY_EDITOR
[CustomEditor(typeof(Boss))]
public class BossEditor : Editor
{
	private SerializedProperty maxHealth;
	private SerializedProperty actions;

	private void OnEnable()
	{
		// Setup the SerializedProperties.
		maxHealth = serializedObject.FindProperty("maxHealth");
		actions   = serializedObject.FindProperty("actions");
	}

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		return;
		serializedObject.Update();

		if (!maxHealth.hasMultipleDifferentValues)
			EditorGUILayout.IntField("Max Health", maxHealth.intValue);

		if (!actions.hasMultipleDifferentValues)
		{
			EditorGUILayout.PropertyField(actions);

			var boss = (Boss) target;
			for (int i = 0; i < boss.actions.Count; i++)
			{
				if (boss.actions[i] is null)
					continue;

				EditorGUILayout.BeginVertical("Window");
				boss.actions[i] = (BossAction) EditorGUILayout.ObjectField("Action", boss.actions[i], typeof(BossAction), true);
				boss.actions[i].DrawUi();
				EditorGUILayout.EndVertical();
				EditorGUILayout.Separator();
			}
		}

		serializedObject.ApplyModifiedProperties();
	}
}
# endif