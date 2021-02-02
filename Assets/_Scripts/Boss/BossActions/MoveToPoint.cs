using System.Collections;
using UnityEditor;
using UnityEngine;

public class MoveToPoint : BossAction
{
	[SerializeField] private GameObject objectToMove;
	[SerializeField] public Vector3 point = Vector3.zero;
	[SerializeField] private float maxDistanceDelta = 1F;
	[SerializeField] private bool useRigidBody = false;
	[SerializeField] private Rigidbody2D rigidbody2DComponent = null;
	[SerializeField] private float speedMultiplier = 1F;
	
	public override IEnumerator DoAction()
	{
		while (CheckPosition())
		{
			if (!useRigidBody)
				objectToMove.transform.position = Vector2.MoveTowards(objectToMove.transform.position, point, maxDistanceDelta * Time.deltaTime);
			else
			{
				var oldVelocity = rigidbody2DComponent.velocity;

				var position = objectToMove.transform.position;
				var speed = -((position - point).normalized) * speedMultiplier;
				rigidbody2DComponent.AddForce(speed, ForceMode2D.Impulse);

				var duration = Vector2.Distance(position, point) / speedMultiplier;
				
				yield return new WaitForSeconds(duration);
				rigidbody2DComponent.velocity = oldVelocity;
			}
			
			
			yield return new WaitForEndOfFrame();
		}
	}

	private bool CheckPosition()
	{
		if (!canRun)
			return false;
		
		var position = objectToMove.transform.position;
		return Vector2.Distance(position, point) > 1;
	}

	# if UNITY_EDITOR
	public override void DrawUi()
	{
		base.DrawUi();

		objectToMove = (GameObject)EditorGUILayout.ObjectField("Object To Move", objectToMove, typeof(GameObject), true);
		point = EditorGUILayout.Vector3Field("Point", point);
		maxDistanceDelta = EditorGUILayout.FloatField("Max Distance Delta (!RB)", maxDistanceDelta);
		useRigidBody = EditorGUILayout.Toggle("Use Rigidbody Velocity", useRigidBody);
		rigidbody2DComponent = (Rigidbody2D) EditorGUILayout.ObjectField("RB2D Component", rigidbody2DComponent, typeof(Rigidbody2D), true);
		speedMultiplier = EditorGUILayout.FloatField("Speed Multiplier", speedMultiplier);
	}
	# endif
}
