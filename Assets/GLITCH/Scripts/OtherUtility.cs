using UnityEngine;

namespace GLITCH.Utilities
{
	public class OtherUtility {
		
		public static void SendMessageToAll(string message)
		{
			GameObject[] gos = (GameObject[])GameObject.FindObjectsOfType(typeof(GameObject));
			foreach (GameObject go in gos)
			{
				if (go && go.transform.parent == null)
				{
					go.gameObject.BroadcastMessage(message, SendMessageOptions.DontRequireReceiver);
				}
			}
		}
	}
}
