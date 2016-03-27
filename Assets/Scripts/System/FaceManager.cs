using UnityEngine;
using System.Collections;
using Data;

public class FaceManager : MonoBehaviour {

	public FaceData playerFaces;
	public FaceData enemyFaces;

	public enum Type
	{
		Player,
		Enemy
	}

	public Face GetFace(int id, Type type)
	{
		if (type == Type.Player)
		{
			return playerFaces.faces.Find(x => x.ID == id);
		}
		else
		{
			return enemyFaces.faces.Find(x => x.ID == id);
		}
	}

	public Face GetFace(string name, Type type)
	{
		if (type == Type.Player)
		{
			return playerFaces.faces.Find(x => x.name == name);
		}
		else
		{
			return enemyFaces.faces.Find(x => x.name == name);
		}
	}
}
