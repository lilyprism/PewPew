using System.Collections;
using UnityEngine;

public class RotateToMovement : BossAction
{
	[SerializeField] private float rotationSpeed = 1f;
	[SerializeField] private GameObject objectToRotate;
	[SerializeField] private float rotationModifier = 1f;
	
	public override IEnumerator DoAction()
	{
		var rb = objectToRotate.GetComponent<Rigidbody2D>();
		while (canRun)
		{
			var direction = rb.velocity.normalized;
			var rotation = Quaternion.LookRotation(Vector3.forward, direction * rotationModifier);
			var tempRot = Quaternion.RotateTowards(objectToRotate.transform.rotation, rotation, rotationSpeed * Time.deltaTime);
			objectToRotate.transform.rotation = tempRot;
			
			yield return new WaitForEndOfFrame();
		}
	}

	# if UNITY_EDITOR
	public override void DrawUi()
	{
		base.DrawUi(); 
	}
	#endif
}
