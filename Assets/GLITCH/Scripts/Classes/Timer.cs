using UnityEngine;
using UnityEngine.UI;
using System.Timers;

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
		public bool finished;

		bool isRed = false;
		bool isFlashing = false;

		public enum Format
		{
			AnyDigit,
			AnyDigitWithColon,
			DoubleDigitWithColon
		}

		public void StartTimer(float time)
		{
			startTime = time;
			currentTime = time;
			UpdateText();
			InvokeRepeating("DecreaseTimeRemaining", 1.0f, 1.0f);
			hasStarted = true;
			finished = false;
		}

		void DecreaseTimeRemaining()
		{
			if (currentTime <= 0)
			{
				StopTimer();
				return;
			}
			else if(currentTime <= flashStartTime && flash && !isFlashing)
			{
				InvokeRepeating("Flash", 0.001f, 0.5f);
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
			isRed = !isRed;
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
			currentTime = startTime;
			hasStarted = false;
			finished = true;
			BroadcastMessage("StopTimer");
		}
	}
}
