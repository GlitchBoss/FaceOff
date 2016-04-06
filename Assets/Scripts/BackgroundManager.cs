using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackgroundManager : MonoBehaviour {

	public BackgroundFace[] faces;
	public int maxTeamSize = 2;
	public float spawnDelay;
	public Transform[] spawnpoints;
	public List<BackgroundFace> team1 = new List<BackgroundFace>();
	public List<BackgroundFace> team2 = new List<BackgroundFace>();

	bool canSpawn = true;

	void Start()
	{
		//Spawn first two faces here

		int[] i = RandomFaceAndSpawnPoint();
		BackgroundFace face1 = (BackgroundFace)Instantiate(faces[i[0]], spawnpoints[i[1]].position, Quaternion.identity);
		face1.BM = this;
		face1.team = 0;
		face1.teamIndicator.color = GameManager.instance.teamColors[0];

		i = RandomFaceAndSpawnPoint();
		BackgroundFace face2 = (BackgroundFace)Instantiate(faces[i[0]], spawnpoints[i[1]].position, Quaternion.identity);
		face2.BM = this;
		face2.team = 1;
		face2.teamIndicator.color = GameManager.instance.teamColors[1];

		face2.target = face1;
		face1.target = face2;

		team1.Add(face1);
		team2.Add(face2);

		StartCoroutine(DelaySpawn(spawnDelay));

		//Add ability to get a new target to Background Face script
	}

	void Update()
	{
		if (canSpawn)
		{
			Spawn();
		}
	}

	void Spawn()
	{
		int team = ChooseTeam();
		if (team == 0 && team1.Count >= maxTeamSize)
		{
			return;
		}
		else if (team == 1 && team2.Count >= maxTeamSize)
		{
			return;
		}
		int[] i = RandomFaceAndSpawnPoint();
		BackgroundFace face = (BackgroundFace)Instantiate(faces[i[0]], spawnpoints[i[1]].position, Quaternion.identity);
		face.BM = this;
		face.team = team;
		face.teamIndicator.color = GameManager.instance.teamColors[team];
		face.target = ChooseTarget(team);
		if(team == 0)
		{
			team1.Add(face);
		}
		else
		{
			team2.Add(face);
		}
		StartCoroutine(DelaySpawn(spawnDelay));
	}

	int ChooseTeam()
	{
		if(team1.Count > team2.Count)
		{
			return 1;
		}
		else if(team1.Count < team2.Count)
		{
			return 0;
		}
		else
		{
			return Random.Range(0, 1);
		}
	}

	public BackgroundFace ChooseTarget(int yourTeam)
	{
		int i;
		if (yourTeam == 1)
		{
			if(team1.Count <= 0)
			{
				return null;
			}
			i = Random.Range(0, team1.Count);
			return team1[i];
		}
		else if (yourTeam == 0)
		{
			if (team2.Count <= 0)
			{
				return null;
			}
			i = Random.Range(0, team2.Count);
			return team2[i];
		}
		else
		{
			return null;
		}
	}

	int[] RandomFaceAndSpawnPoint()
	{
		int x = Random.Range(0, faces.Length);
		int y = Random.Range(0, spawnpoints.Length);
		return new int[2] { x, y };
	}

	IEnumerator DelaySpawn(float delay)
	{
		canSpawn = false;
		yield return new WaitForSeconds(delay);
		canSpawn = true;
	}
}
