using UnityEngine;
using System.Collections;
using Data;

public class UnlockManager : MonoBehaviour {

	int unlocked;
	public FaceData faces;

	void Start()
	{
		unlocked = PlayerPrefs.GetInt("AIUnlocked", 1);

		for(int i = 0; i < unlocked; i++)
		{
			faces.faces[i].unlocked = true;
		}
	}
}
