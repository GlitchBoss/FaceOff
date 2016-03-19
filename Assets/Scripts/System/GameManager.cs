using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Analytics;

public class GameManager : MonoBehaviour {

    public Character[] faces;
	public Character[] enemies;
	public bool singlePlayer;
	public float gameTime;
	public int[] score;

    [HideInInspector]
    public int player1Face, player2Face, enemyFace;
	[HideInInspector]
	public List<Character> players;
	[HideInInspector]
	public UIManager UIM;

    GameObject[] spawnPoints1, spawnPoints2;
	float time;

    public static GameManager instance;

    void Awake()
    {
        if (!instance)
            instance = this;
        if (instance != this)
            Destroy(gameObject);
        StartUp();
    }

    void OnLevelWasLoaded()
    {
        StartUp();
    }

    void StartUp()
    {
        UIM = GameObject.Find("UIManager").GetComponent<UIManager>();
        switch (SceneManager.GetActiveScene().name)
        {
            case "Arena":
                players = new List<Character>();
                SpawnPlayers(singlePlayer = false);
				UIM.StartTimer(time);
				UIM.UpdateScore(score);
				UIM.SpecialPowerBtn.SetActive(false);
                break;
			case "SinglePlayer":
				players = new List<Character>();
				SpawnPlayers(singlePlayer = true);
				UIM.StartTimer(time);
				UIM.UpdateScore(score);
				UIM.SpecialPowerBtn.SetActive(false);
				break;
			case "MainMenu":
				score[0] = 0;
				score[1] = 0;
				time = gameTime;
				break;
        }
    }

    void SpawnPlayers(bool singlePlayer)
    {
		players.Clear();
        spawnPoints1 = GameObject.FindGameObjectsWithTag("SpawnPoint1");
        spawnPoints2 = GameObject.FindGameObjectsWithTag("SpawnPoint2");
        int index = Random.Range(0, spawnPoints1.Length);

		Character p1 = (Character)Instantiate(faces[player1Face], 
            spawnPoints1[index].transform.position, Quaternion.identity);
		Character p2;
		if (singlePlayer)
			p2 = (Character)Instantiate(enemies[enemyFace], 
				spawnPoints2[index].transform.position, Quaternion.identity);
		else
		{
			p2 = (Character)Instantiate(faces[player2Face],
				spawnPoints2[index].transform.position, Quaternion.identity);
		}
		p1.useSecondaryControls = true;
        p1.health.healthSlider = UIM.sliders[0];
        p1.power.powerSlider = UIM.sliders[2];
        p2.health.healthSlider = UIM.sliders[1];
        p2.power.powerSlider = UIM.sliders[3];
        p1.SetControls();
        p2.SetControls();
        players.Add(p1);
        players.Add(p2);
    }

	public void AddScore(Character loser)
	{
		int index = players.IndexOf(loser);
		if(index == 0)
		{
			score[1]++;
			players[1].Win();
		}
		else
		{
			score[0]++;
			players[0].Win();
		}
		//UIM.UpdateScore(score);
		//SpawnPlayers(singlePlayer);
		time = UIM.timer.currentTime;
		StartCoroutine(Restart(1));
	}

	public void Reset()
	{
		score[0] = 0;
		score[1] = 0;
		time = gameTime;
	}

	IEnumerator Restart(float delay)
	{
		yield return new WaitForSeconds(delay);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
