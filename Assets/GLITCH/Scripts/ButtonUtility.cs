using UnityEngine;
using UnityEngine.SceneManagement;

namespace GLITCH.Utilities
{
	public class ButtonUtility {

		public static void Start_Game()
		{
			Time.timeScale = 1.0f;
			Time.fixedDeltaTime = 0.02f;
			OtherUtility.SendMessageToAll("OnStartGame");
		}

		public static void Pause_Game()
		{
			Time.timeScale = 0;
			Time.fixedDeltaTime = 0.0f;
			OtherUtility.SendMessageToAll("OnPauseGame");
		}

		public static void Resume_Game()
		{
			Time.timeScale = 1.0f;
			Time.fixedDeltaTime = 0.02f;
			OtherUtility.SendMessageToAll("OnResumeGame");
		}

		public static void ChangeTimeSpeed(float newSpeed)
		{
			Time.timeScale = newSpeed;
			Time.fixedDeltaTime = 0.02f;
		}
	}
}
