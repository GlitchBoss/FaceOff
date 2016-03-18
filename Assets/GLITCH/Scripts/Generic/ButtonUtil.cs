using UnityEngine;
using UnityEngine.SceneManagement;

namespace GLITCH.Generic
{
	public class ButtonUtil : MonoBehaviour
	{

		public static void Restart_Game()
		{
			Load_Scene(SceneManager.GetActiveScene().name);
		}

		public static void Load_Scene(int scene)
		{
			SceneManager.LoadScene(scene);
		}

		public static void Load_Scene(string scene)
		{
			SceneManager.LoadScene(scene);
		}

		public static void Load_Scene_Additive(int scene)
		{
			SceneManager.LoadScene(scene, LoadSceneMode.Additive);
		}

		public static void Load_Scene_Additive(string scene)
		{
			SceneManager.LoadScene(scene, LoadSceneMode.Additive);
		}
	}
}
