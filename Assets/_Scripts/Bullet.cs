using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField] private float secondsToLive = 1;
	[SerializeField] private float velocity = 10;
	private Weapon weapon;
	
	public virtual void OnShootStart(Weapon weapon)
	{
		this.weapon = weapon;
		
		var rb = gameObject.GetComponent<Rigidbody2D>();
		rb.velocity = new Vector2(0, velocity);

		StartCoroutine(SelfDelete());
	}

	protected virtual IEnumerator SelfDelete()
	{
		yield return new WaitForSeconds(secondsToLive);
		Destroy(gameObject);
	}

	protected virtual void OnTriggerEnter2D(Collider2D other)
	{
		if (!other.gameObject.CompareTag("Enemy"))
			return;

		var enemy = other.GetComponent<Enemy>();
		enemy.GetHit(weapon.TrueDamage());
		
		StopAllCoroutines();
		Destroy(gameObject);
	}
}
