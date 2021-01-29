using UnityEngine.UIElements;

[System.Serializable]
public class Stats
{
	public int power;
	public int critical;
	public int stamina;
	public int speed;

	public int Total()
	{
		return power + critical + stamina + speed;
	}

	public override string ToString()
	{
		return $"Power: {power}\tCritical: {critical}\tStamina: {stamina}\tSpeed: {speed}";
	}

	// Override Addition operator for calculation purposes
	public static Stats operator +(Stats a, Stats b)
	{
		return new Stats {power = a.power + b.power, critical = a.critical + b.critical, stamina = a.stamina + b.stamina, speed = a.speed + b.speed};
	}
}
