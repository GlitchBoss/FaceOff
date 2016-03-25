using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GLITCH.Helpers;

public class CharacterHealth : MonoBehaviour {

	public RangeFloat health;

	[HideInInspector]
	public Slider healthSlider;

	Character character;

	void Start()
	{
		character = GetComponent<Character>();
		health.num = health.max;
	}

	public void UpdateHealthSlider()
	{
		if (character.paused)
			return;
		healthSlider.maxValue = health.max;
		healthSlider.minValue = health.min;
		healthSlider.value = health.num;
	}

	public void LoseHealth(float amount)
	{
		if (character.lost || character.paused)
			return;
		health.num -= amount;
		if (health.num <= 0)
		{
			character.Lose();
		}
		UpdateHealthSlider();
	}

	public void GainHealth(float amount)
	{
		health.num += amount;
		UpdateHealthSlider();
	}
}
