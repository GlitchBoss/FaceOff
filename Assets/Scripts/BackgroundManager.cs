using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackgroundManager : MonoBehaviour {

	public BackgroundFace[] faces;
	public int maxTeamSize = 2;
	public Transform[] spawnpoints1;
	public Transform[] spawnpoints2;
	public List<BackgroundFace> team1 = new List<BackgroundFace>();
	public List<BackgroundFace> team2 = new List<BackgroundFace>();

	void Start()
	{
		ReplenishTeams();

		ChooseTargets();
	}

	public void ReplenishTeams()
	{
		int[] x = RandomFaceAndSpawnPoint();
		int dif = maxTeamSize - team1.Count;
		for(int i = 0; i < dif; i++)
		{
			BackgroundFace face = (BackgroundFace)Instantiate(faces[x[0]], spawnpoints1[x[1]].position, Quaternion.identity);
			face.BM = this;
			team1.Add(face);
			x = RandomFaceAndSpawnPoint();
		}

		x = RandomFaceAndSpawnPoint();
		dif = maxTeamSize - team2.Count;
		for (int i = 0; i < dif; i++)
		{
			BackgroundFace face = (BackgroundFace)Instantiate(faces[x[0]], spawnpoints1[x[1]].position, Quaternion.identity);
			face.BM = this;
			team2.Add(face);
			x = RandomFaceAndSpawnPoint();
		}
		ChooseTargets();
		Debug.Log("Replenished");
	}

	public void ChooseTargets()
	{
		if(team1.Count != team2.Count)
		{
			ReplenishTeams();
		}
		for (int i = 0; i < team1.Count; i++)
		{
			int x = Random.Range(0, team1.Count);
			team1[i].target = team2[x];
			team2[i].target = team1[x];
		}
	}

	int[] RandomFaceAndSpawnPoint()
	{
		int x = Random.Range(0, faces.Length);
		int y = Random.Range(0, spawnpoints1.Length);
		return new int[2] { x, y };
	}
}
