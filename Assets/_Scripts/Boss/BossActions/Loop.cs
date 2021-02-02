using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Loop : BossAction
{
	[SerializeField] public  List<BossAction> actions;
	[SerializeField] private bool             checkOnlyAfterEachRotation = false;
	[SerializeField] private bool             loopForTimes               = true;
	[SerializeField] private int              executions                 = 1;
	[SerializeField] private bool             checkForTriggerHealth      = false;
	[SerializeField] private int              bossTriggerHealthPercent   = 50;
	[SerializeField] private bool             checkForBelowTriggerHealth = false;

	public override IEnumerator DoAction()
	{
		while (canRun)
		{
			foreach (var bossAction in actions)
			{
				Debug.Log($"Running action {bossAction.GetType()}", bossAction);

				if (bossAction.waitForEnd)
					yield return bossAction.DoAction();
				else
					StartCoroutine(bossAction.DoAction());

				if (!checkOnlyAfterEachRotation)
					if (Check())
						yield break;
			}

			if (loopForTimes)
				executions -= 1;

			if (Check())
				yield break;
		}
	}

	private bool Check()
	{
		if (loopForTimes && executions <= 0 || !canRun)
			return true;

		if (!checkForTriggerHealth) return false;

		if (checkForBelowTriggerHealth)
			return HealthPercent() < bossTriggerHealthPercent;

		return HealthPercent() > bossTriggerHealthPercent;
	}

	private int HealthPercent()
	{
		var health    = gameObject.GetComponent<Boss>().health;
		var maxHealth = gameObject.GetComponent<Boss>().maxHealth;

		return (int) ((float) health / (float) maxHealth * 100F);
	}

	# if UNITY_EDITOR
	public override void DrawUi()
	{
		base.DrawUi();

		loopForTimes               = EditorGUILayout.Toggle("Loop For Numbers", loopForTimes);
		executions                 = EditorGUILayout.IntField("Executions", executions);
		checkForTriggerHealth      = EditorGUILayout.Toggle("Check for Health Trigger", checkForTriggerHealth);
		bossTriggerHealthPercent   = EditorGUILayout.IntSlider("Trigger Health Percent", bossTriggerHealthPercent, 1, 99);
		checkForBelowTriggerHealth = EditorGUILayout.Toggle("Check Below Trigger",            checkForBelowTriggerHealth);
		checkOnlyAfterEachRotation = EditorGUILayout.Toggle("Check Only After Each Rotation", checkOnlyAfterEachRotation);

		var obj = new SerializedObject(this);
		EditorGUILayout.PropertyField(obj.FindProperty("actions"));

		for (int i = 0; i < actions.Count; i++)
		{
			if (actions[i] is null)
				continue;

			EditorGUILayout.BeginVertical("Window");
			actions[i] = (BossAction) EditorGUILayout.ObjectField("Action", actions[i], typeof(BossAction), true);
			actions[i].DrawUi();
			EditorGUILayout.EndVertical();
			EditorGUILayout.Separator();
		}
	}
	#endif
}
