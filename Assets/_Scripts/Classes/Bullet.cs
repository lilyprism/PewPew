using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField] private float secondsToLive = 1;
	[SerializeField] private float velocity = 10;
	[SerializeField] public int damage = 10;
	[SerializeField] private bool damageEnemies = false;
	
	public virtual void OnShootStart()
	{
		
		var rb = gameObject.GetComponent<Rigidbody2D>();
		rb.AddForce(transform.up * velocity);

		transform.parent = null;
		StartCoroutine(SelfDelete());
	}

	protected virtual IEnumerator SelfDelete()
	{
		yield return new WaitForSeconds(secondsToLive);
		Destroy(gameObject);
	}

	protected virtual void OnTriggerEnter2D(Collider2D other)
	{
		var hitable = other.gameObject.GetComponent<IHitable>();
		
		if (hitable == null)
			return;
		
		if (!damageEnemies && other.gameObject.CompareTag("Enemy"))
			return;
		
		if (damageEnemies && other.gameObject.CompareTag("Player"))
			return;
		
		hitable.GetHit(damage);
		StopAllCoroutines();
		Destroy(gameObject);
	}
}
