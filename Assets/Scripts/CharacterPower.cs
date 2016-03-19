using UnityEngine;
using System.Collections;
using GLITCH.Helpers;
using UnityEngine.UI;

public class CharacterPower : MonoBehaviour {

	public RangeFloat power;
	[Tooltip("Seconds")]
	public float timeUntilFull;

	[HideInInspector]
	public Slider powerSlider;

	Character character;
	float powerDecrease;
	float powerIncrease;

	void Start()
	{
		character = GetComponent<Character>();
		power.num = power.min;
		power.max = 100;
		if (character.SP.duration >= 1)
		{
			powerDecrease = power.max / character.SP.duration;
		}
		else
		{
			powerDecrease = power.max;
		}
		powerIncrease = power.max / timeUntilFull;
	}

	public void UpdatePowerSlider(bool inUse)
	{
		if (character.lost || character.paused)
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
		powerSlider.value = power.num;
	}
}
