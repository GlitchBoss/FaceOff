using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GLITCH.Helpers;

public class CharacterHealth : MonoBehaviour {

	public RangeFloat health;
	public bool background = false;

	[HideInInspector]
	public Slider healthSlider;

	Character character;
	BackgroundFace face;

	void Start()
	{
		if (background)
		{
			face = GetComponent<BackgroundFace>();
		}
		else
		{
			character = GetComponent<Character>();
		}
		health.num = health.max;
	}

	public void UpdateHealthSlider()
	{
		if (!character || character.paused || background)
			return;
		healthSlider.maxValue = health.max;
		healthSlider.minValue = health.min;
		healthSlider.value = health.num;
	}

	public void LoseHealth(float amount)
	{
		if (character && (character.lost || character.paused) && !background)
			return;
		health.num -= amount;
		if (health.num <= 0)
		{
			if (background)
			{
				face.Lose();
			}
			else
			{
				character.Lose();
			}
		}
		UpdateHealthSlider();
	}

	public void GainHealth(float amount)
	{
		health.num += amount;
		UpdateHealthSlider();
	}
}
