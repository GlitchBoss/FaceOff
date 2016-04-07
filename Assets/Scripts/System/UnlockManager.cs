using UnityEngine;
using System.Collections;
using Data;

public class UnlockManager : MonoBehaviour {

	public FaceData ai;
	public FaceData faces;

	void Awake()
	{
		PlayerPrefs.SetInt(ai.faces[0].ID.ToString() + "Unlocked", 1);
		PlayerPrefs.SetInt(faces.faces[0].ID.ToString() + "Owned", 1);
	}
}
