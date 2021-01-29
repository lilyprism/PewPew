using UnityEngine;

public class Item : ScriptableObject
{
	public new string     name = "New Item";
	public     Stats      stats;
	public     ItemRarity itemRarity;
}

public enum ItemRarity
{
	White,
	Green,
	Blue,
	Purple,
	Orange
}
