using System.Collections;
using UnityEngine;

public class RotateToObject : BossAction
{
	[SerializeField] private float      rotationSpeed = 1f;
	[SerializeField] private GameObject objectToRotate;
	[SerializeField] private GameObject objectToRotateTo;
	[SerializeField] private float      rotationModifier = 1f;

	public override IEnumerator DoAction()
	{
		while (canRun)
		{
			var direction = objectToRotateTo.transform.position - objectToRotate.transform.position;
			var rotation  = Quaternion.LookRotation(Vector3.forward, direction                                  * rotationModifier);
			var tempRot   = Quaternion.RotateTowards(objectToRotate.transform.rotation, rotation, rotationSpeed * Time.deltaTime);
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
