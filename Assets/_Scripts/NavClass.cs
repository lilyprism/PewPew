using System;
using System.Collections;
using UnityEngine;

public abstract class NavClass : MonoBehaviour
{
	[SerializeField] public Stats    baseStats;
	[SerializeField] public NavItems items;
	[SerializeField] public bool canShoot;

	public int baseHealth = 1000;
	public int health = 1000;
	public GameObject carapace;
	public GameObject booster1;
	public GameObject booster2;
	public GameObject engine;
	public GameObject weapon1;
	public GameObject weapon2;

	private Rigidbody2D rb;

	public virtual void Start()
	{
		health = TrueMaxHealth();
	}

	public virtual void Awake()
	{
		rb = this.GetComponent<Rigidbody2D>();
		Debug.Log("Current Spaceship has stats:\t" + TotalStats());
		
		StartCoroutine(Shoot(items.weapon1, weapon1));
		StartCoroutine(Shoot(items.weapon2, weapon2));
	}

	protected virtual void Update()
	{
		var horizontal = Input.GetAxis("Horizontal") * TotalStats().speed;
		var vertical = Input.GetAxis("Vertical") * TotalStats().speed;
		rb.velocity = new Vector2(horizontal, vertical);
	}

	protected virtual IEnumerator Shoot(Weapon item, GameObject shipWeapon)
	{
		if (!item || !shipWeapon)
			yield break;
		
		item.Initialize(this);
		
		while (true)
		{
			if (canShoot)
				item.Shoot(shipWeapon);

			yield return new WaitForSeconds(1 / item.shootsPerSecond);
		}
	}

	public virtual void Ability1()
	{
	}

	public virtual void Ability2()
	{
	}

	public int TrueMaxHealth()
	{
		return baseHealth * (TotalStats().stamina / 3);
	}
	
	public Stats TotalStats()
	{
		return baseStats + items.StatSum();
	}

	private bool CanShoot()
	{
		return items.weapon1 || items.weapon2;
	}
}

[Serializable]
public class NavItems
{
	public Booster booster1;
	public Booster booster2;
	public Carapace carapace;
	public Weapon weapon1;
	public Weapon weapon2;
	public Engine engine;

	public Stats StatSum()
	{
		var sum = new Stats();

		if (booster1)
			sum += booster1.stats;

		if (booster2)
			sum += booster2.stats;

		if (carapace)
			sum += carapace.stats;

		if (weapon1)
			sum += weapon1.stats;

		if (weapon2)
			sum += weapon2.stats;

		if (engine)
			sum += engine.stats;

		return sum;
	}
}
