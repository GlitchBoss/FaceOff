using UnityEngine;
using System.Collections;
using System;

public class Smiley : Player, IUnlockable
{
	public bool unlocked;

	public void CheckUnlock()
	{
		Unlock();
	}

	public void Unlock()
	{
		FileIO.WriteStringToFile("Smiley", "UnlockedFaces");
	}
}
