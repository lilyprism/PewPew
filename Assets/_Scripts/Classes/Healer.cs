using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Healer : NavClass
{
	[SerializeField] private GameObject shockwavePrefab;
	[SerializeField] private float      shockwaveRatio = 1;

	public override void Ability1()
	{
		base.Ability1();

		StartCoroutine(DoShockwave());
	}

	private IEnumerator DoShockwave()
	{
		var wave = Instantiate(shockwavePrefab, transform.position, Quaternion.identity);

		wave.GetComponent<ShockwaveController>().shockwaveDamage = (int) (TotalStats().power * 5.5);
		var collider = wave.GetComponent<CircleCollider2D>();
		var light    = wave.GetComponent<Light2D>();


		while (collider.radius < 20)
		{
			var temp = Vector3.MoveTowards(Vector3.one * collider.radius, new Vector3(50, 50, 1), shockwaveRatio);

			collider.radius             = temp.x;
			light.pointLightOuterRadius = temp.x;
			yield return new WaitForEndOfFrame();
		}

		Destroy(wave);
	}
}
