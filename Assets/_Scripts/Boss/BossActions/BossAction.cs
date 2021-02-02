using System.Collections;
using UnityEditor;
using UnityEngine;

public abstract class BossAction : MonoBehaviour
{
	[SerializeField] public bool waitForEnd = true;
	public bool canRun = true;

	public virtual IEnumerator DoAction()
	{
		yield break;
	}

	# if UNITY_EDITOR
	public virtual void DrawUi()
	{
		waitForEnd = EditorGUILayout.Toggle("Wait For End", waitForEnd);
	}
	#endif
}