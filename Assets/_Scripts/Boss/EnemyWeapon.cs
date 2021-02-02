using UnityEngine;

public class EnemyWeapon : MonoBehaviour, IShootable
{
	[SerializeField] private GameObject bullet;
	[SerializeField] private Vector2    shootablePoint;

	public void Shoot()
	{
		var obj = Instantiate(bullet, shootablePoint, transform.parent.rotation, transform);
		obj.transform.localPosition = shootablePoint;

		var bulletScript = obj.GetComponent<Bullet>();
		bulletScript.OnShootStart();
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawSphere(transform.TransformPoint(shootablePoint), .1f);
	}
}
