using System.Collections;
using UnityEngine;

public class CancelAction : BossAction
{
	[SerializeField] private BossAction actionToCancel;

	public override IEnumerator DoAction()
	{
		if (!actionToCancel) yield break;

		actionToCancel.canRun = false;
	}

	# if UNITY_EDITOR
	public override void DrawUi()
	{
		base.DrawUi();
	}
	#endif
}
