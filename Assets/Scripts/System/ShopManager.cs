using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Data;

public class ShopManager : MonoBehaviour {

	[Range(0, 100)]
	public float chanceToWin;
	public float chanceDecrease;
	public FaceData faces;
	public Image winImage;
	public Sprite loseSprite;

	List<Face> facesToWin = new List<Face>();
	bool isSpinning;

	public void Spin()
	{
		if (isSpinning)
			return;
		isSpinning = true;
		int chance = Random.Range(0, 100);
		if(chance <= chanceToWin)
		{
			WinFace();
		}
		else
		{
			winImage.sprite = loseSprite;
		}
		isSpinning = false;
	}

	void WinFace()
	{
		facesToWin = GetFaces();
		if(facesToWin == null || facesToWin.Count == 0)
		{
			winImage.sprite = loseSprite;
			return;
		}
		int index = Random.Range(0, facesToWin.Count);
		winImage.sprite = facesToWin[index].image;
		PlayerPrefs.SetInt(facesToWin[index].ID.ToString() + "Owned", 1);
		chanceToWin -= chanceDecrease;
	}

	List<Face> GetFaces()
	{
		List<Face> list = new List<Face>();
		int unlocked;

		foreach (Face face in faces.faces)
		{
			unlocked = PlayerPrefs.GetInt(face.ID.ToString() + "Owned", 0);
			if(unlocked != 1)
			{
				list.Add(face);
			}
		}

		if(list.Count == 0)
		{
			return null;
		}
		return list;
	}
}
