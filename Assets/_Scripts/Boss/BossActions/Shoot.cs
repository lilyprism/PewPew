using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Shoot : BossAction
{
	[SerializeField] private List<EnemyWeapon> weapons;
	[SerializeField] private int               shootsPerSeconds;
	[SerializeField] private bool              shootEvenly = true;

	public override IEnumerator DoAction()
	{
		while (canRun)
		{
			foreach (var weapon in weapons)
			{
				weapon.Shoot();

				if (!shootEvenly)
					yield return new WaitForSeconds(1 / (float) shootsPerSeconds / weapons.Count);
			}

			if (shootEvenly)
				yield return new WaitForSeconds(1 / (float) shootsPerSeconds);
		}
	}

	# if UNITY_EDITOR
	public override void DrawUi()
	{
		base.DrawUi();

		shootsPerSeconds = EditorGUILayout.IntField("Shoots Per Second", shootsPerSeconds);
		shootEvenly      = EditorGUILayout.Toggle("Shoot Evenly", shootEvenly);

		var obj = new SerializedObject(this);
		EditorGUILayout.PropertyField(obj.FindProperty("weapons"));
	}
	#endif
}
