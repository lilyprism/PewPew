using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
	[SerializeField] private NavClass ship;
	[SerializeField] private Slider   healthSlider;

	private void Start()
	{
		if (healthSlider && ship)
			healthSlider.maxValue = ship.TrueMaxHealth();
	}

	private void Update()
	{
		healthSlider.value = ship.health;
	}
}
