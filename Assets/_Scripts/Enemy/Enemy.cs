using UnityEngine;

public class Enemy : MonoBehaviour, IHitable
{
	[SerializeField] private int health = 100;

	public virtual void GetHit(int damage)
	{
		Debug.Log($"Got hit for {damage}");
		health -= damage;
		
		if (health<= 0)
			Die();
	}

	public virtual void Die()
	{
		Destroy(gameObject);
	}
}
