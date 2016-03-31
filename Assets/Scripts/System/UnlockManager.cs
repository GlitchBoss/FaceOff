using UnityEngine;
using System.Collections;

public class UnlockManager : MonoBehaviour {

	void Start()
	{
		Smiley smiley = GameManager.instance.FM.GetFace("Smiley", FaceManager.Type.Player).prefab.GetComponent<Smiley>();
		smiley.Unlock();

		/*

		Fix Smiley Errors!
		Create and add script for each face.
		Check for unlock every time the main menu scene is loaded.
		Check for a way to add a line to a file.

		*/
	}
}
