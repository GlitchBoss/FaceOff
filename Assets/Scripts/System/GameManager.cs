using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Analytics;

public class GameManager : MonoBehaviour {

	public bool singlePlayer;
	public float gameTime;
	public int[] score;
	public Color[] teamColors;

    [HideInInspector]
    public int player1Face, player2Face, enemyFace;
	[HideInInspector]
	public List<Character> players;
	[HideInInspector]
	public UIManager UIM;
	[HideInInspector]
	public FaceManager FM;
	[HideInInspector]
	public bool tieBreaker;
	[HideInInspector]
	public ScoreManager SM;

    GameObject[] spawnPoints1, spawnPoints2;
	float time;

    public static GameManager instance;

    void Awake()
    {
        if (!instance)
            instance = this;
        if (instance != this)
            Destroy(gameObject);
		FM = GetComponentInChildren<FaceManager>();
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
				UIM.SpecialPowerBtn.SetActive(false);
				tieBreaker = false;
                break;
			case "SinglePlayer":
				players = new List<Character>();
				SM = FindObjectOfType<ScoreManager>();
				SpawnPlayers(singlePlayer = true);
				UIM.SpecialPowerBtn.SetActive(false);
				ScoreManager.OnGameOver -= EndGame;
				ScoreManager.OnGameOver += EndGame;
				tieBreaker = false;
				break;
			case "MainMenu":
				score[0] = 0;
				score[1] = 0;
				time = gameTime;
				tieBreaker = false;
				break;
        }
    }

    void SpawnPlayers(bool singlePlayer)
    {
		players.Clear();
        spawnPoints1 = GameObject.FindGameObjectsWithTag("SpawnPoint1");
        spawnPoints2 = GameObject.FindGameObjectsWithTag("SpawnPoint2");
        int index = Random.Range(0, spawnPoints1.Length);

		Character p1 = (Character)Instantiate(FM.GetFace(player1Face, FaceManager.Type.Player).prefab, 
            spawnPoints1[index].transform.position, Quaternion.identity);
		Character p2;
		if (singlePlayer)
			p2 = (Character)Instantiate(FM.GetFace(enemyFace, FaceManager.Type.Enemy).prefab, 
				spawnPoints2[index].transform.position, Quaternion.identity);
		else
		{
			p2 = (Character)Instantiate(FM.GetFace(player2Face, FaceManager.Type.Player).prefab,
				spawnPoints2[index].transform.position, Quaternion.identity);
		}
		p1.useSecondaryControls = true;
        p1.health.healthSlider = UIM.sliders[0];
        p1.power.powerSlider = UIM.sliders[2];
        p2.health.healthSlider = UIM.sliders[1];
        p2.power.powerSlider = UIM.sliders[3];
		p1.teamIndicator.color = teamColors[0];
		p2.teamIndicator.color = teamColors[1];
        p1.SetControls();
        p2.SetControls();
        players.Add(p1);
        players.Add(p2);
    }

	public void AddScore(Character loser)
	{
		int index = players.IndexOf(loser);
		SM.AddScore(index == 0 ? 1 : 0);
		//if(index == 0)
		//{
		//	score[1]++;
		//	players[1].Win();

		//}
		//else
		//{
		//	score[0]++;
		//	players[0].Win();
		//}
		//if (tieBreaker)
		//{
		//	UIM.EndGame();
		//	return;
		//}
		time = UIM.timer.currentTime;
		StartCoroutine(Restart(1));
	}

	public void EndGame()
	{
		UIM.EndGame();
		ScoreManager.OnGameOver -= EndGame;
	}

	public void Reset()
	{
		score[0] = 0;
		score[1] = 0;
		time = gameTime;
		tieBreaker = false;
	}

	IEnumerator Restart(float delay)
	{
		yield return new WaitForSeconds(delay);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
