using System;
using UnityEngine;

public class ShockwaveController : MonoBehaviour
{
	public int shockwaveDamage = 10;
	
	public void OnTriggerEnter2D(Collider2D other)
	{
		if (!other.CompareTag("Enemy"))
			return;

		var enemy = other.GetComponent<Enemy>();
		enemy.GetHit(shockwaveDamage);
	}
}
