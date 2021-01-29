using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField] private int health = 100;

	public virtual void GetHit(int damage)
	{
		Debug.Log($"Got hit for {damage}");
		health -= damage;
		
		if (health<= 0)
			Die();
	}

	protected virtual void Die()
	{
		Destroy(gameObject);
	}
}
