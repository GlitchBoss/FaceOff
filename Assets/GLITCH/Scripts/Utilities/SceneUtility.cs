using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;

namespace GLITCH.Utilities
{
	public class SceneUtility : MonoBehaviour
	{
		private static List<GameObject> dontDestroy = new List<GameObject>();
		public static string currentScene = "MainMenu";

		public static void LoadScene(Scene scene)
		{
			foreach (GameObject go in dontDestroy.ToArray())
			{
				UnityEngine.Object.DontDestroyOnLoad(go);
			}
			SceneManager.LoadScene(scene.name);
			foreach (GameObject go in dontDestroy)
			{
				SceneManager.MoveGameObjectToScene(go, scene);
				go.SendMessage("StartUp");
			}
		}

		public static void LoadScene(int build)
		{
			foreach (GameObject go in dontDestroy.ToArray())
			{
				UnityEngine.Object.DontDestroyOnLoad(go);
			}
			SceneManager.LoadScene(build);
			foreach (GameObject go in dontDestroy)
			{
				SceneManager.MoveGameObjectToScene(go, SceneManager.GetSceneAt(build));
				go.SendMessage("StartUp");
			}
		}

		public static void LoadScene(string name)
		{
			foreach (GameObject go in dontDestroy.ToArray())
			{
				UnityEngine.Object.DontDestroyOnLoad(go);
			}
			SceneManager.LoadScene(name);
			currentScene = name;
			foreach (GameObject go in dontDestroy)
			{
				//SceneManager.MoveGameObjectToScene(go, SceneManager.GetSceneByName(name));
				go.SendMessage("StartUp");
			}
		}

		public void MoveAllObjects(string sceneName)
		{
			foreach (GameObject go in dontDestroy)
			{
				SceneManager.MoveGameObjectToScene(go, SceneManager.GetSceneByName(sceneName));
				StartCoroutine(Delay(go));
			}
		}

		IEnumerator Delay(GameObject go)
		{
			yield return new WaitForSeconds(0.1f);
			go.SendMessage("StartUp", SendMessageOptions.DontRequireReceiver);
		}

		public static void DontDestroyOnLoad(GameObject go)
		{
			dontDestroy.Add(go);
		}
	}
}
