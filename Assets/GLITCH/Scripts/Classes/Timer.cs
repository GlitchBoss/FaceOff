using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

namespace GLITCH.Helpers
{
	public class Timer : MonoBehaviour {
		public float startTime;
		public float currentTime;
		public bool hasStarted;
		public bool flash;
		public int flashStartTime;
		public Format format;
		public Text text;

		bool isRed;
		bool isFlashing;

		public enum Format
		{
			AnyDigit,
			AnyDigitWithColon,
			DoubleDigitWithColon
		}

		public Timer(float startTime, bool flash, int flashStartTime, Format format, Text text)
		{
			this.startTime = startTime;
			this.flash = flash;
			this.flashStartTime = flashStartTime;
			this.format = format;
			this.text = text;
			isFlashing = false;
			isRed = false;
		}

		public void StartTimer()
		{
			InvokeRepeating("DecreaseTimeRemaining", 1.0f, 1.0f);
			hasStarted = true;
		}

		void DecreaseTimeRemaining()
		{
			if (currentTime <= 0)
			{
				StopTimer();
				return;
			}
			else if(currentTime <= flashStartTime + 1 && flash && !isFlashing)
			{
				InvokeRepeating("Flash", 1, 0.5f);
			}
			currentTime--;
			UpdateText();
		}

		void Flash()
		{
			if(isRed)
			{
				text.color = Color.black;
			}
			else
			{
				text.color = Color.red;
			}
		}

		void UpdateText()
		{
			string timeText = currentTime.ToString();
			string minutes;
			string seconds;

			switch (format){
				case Format.AnyDigit:
					timeText = currentTime.ToString();
					break;
				case Format.AnyDigitWithColon:
					minutes = Mathf.Floor(currentTime / 60).ToString();
					seconds = Mathf.Floor(currentTime % 60).ToString();
					timeText = string.Format("{0}:{1}", minutes, seconds);
					break;
				case Format.DoubleDigitWithColon:
					minutes = Mathf.Floor(currentTime / 60).ToString("00");
					seconds = Mathf.Floor(currentTime % 60).ToString("00");
					timeText = string.Format("{0:00}:{1:00}", minutes, seconds);
					break;

			}
			text.text = timeText;
		}

		public void StopTimer()
		{
			if (!hasStarted)
				return;
			CancelInvoke("DecreaseTimeRemaining");
			CancelInvoke("Flash");
			UpdateText();
			hasStarted = false;
			currentTime = startTime;
		}
	}
}
