using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Item/Weapon")]
public class Weapon : Item
{
	[SerializeField] private Vector2    shootablePoint  = Vector2.zero;
	[SerializeField] public  float      shootsPerSecond = 1;
	[SerializeField] public  GameObject bullet;
	private                  NavClass   ship;


	public void Initialize(NavClass ship)
	{
		this.ship = ship;
	}

	public void Shoot(GameObject weapon)
	{
		var obj = Instantiate(bullet, shootablePoint, Quaternion.identity, weapon.transform);
		obj.transform.localPosition = shootablePoint;

		var bulletScript = obj.GetComponent<Bullet>();
		bulletScript.OnShootStart(this);
	}

	public int TrueDamage()
	{
		return ship.TotalStats().power;
	}
}
