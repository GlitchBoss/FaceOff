using UnityEngine;
using System.Collections;
using GLITCH.Helpers;
using UnityEngine.UI;

public class CharacterPower : MonoBehaviour {

	public RangeFloat power;
	[Tooltip("Seconds")]
	public float timeUntilFull;
	public bool background;

	[HideInInspector]
	public Slider powerSlider;

	Character character;
	BackgroundFace face;
	float powerDecrease;
	float powerIncrease;

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
		power.num = power.min;
		power.max = 100;
		if (background)
		{
			if (face.SP.duration >= 1)
			{
				powerDecrease = power.max / face.SP.duration;
			}
			else
			{
				powerDecrease = power.max;
			}
		}
		else
		{
			if (character.SP.duration >= 1)
			{
				powerDecrease = power.max / character.SP.duration;
			}
			else
			{
				powerDecrease = power.max;
			}
		}
		powerIncrease = power.max / timeUntilFull;
	}

	public void UpdatePowerSlider(bool inUse)
	{
		if (character && (character.lost || character.paused) && !background)
			return;
		if (power.num > power.max)
		{
			power.num = power.max;
			return;
		}
		else if (power.num < power.min)
		{
			power.num = power.min;
			return;
		}

		if (inUse)
		{
			power.num -= powerDecrease * Time.deltaTime;
		}
		else
		{
			power.num += powerIncrease * Time.deltaTime;
		}

		if(!background)
			powerSlider.value = power.num;
	}
}
