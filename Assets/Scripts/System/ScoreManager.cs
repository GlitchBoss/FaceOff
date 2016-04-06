using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public delegate void GameOver();
	public static event GameOver OnGameOver;

	public Image[] playerScore;
	public Image[] enemyScore;
	public Text winText;
	public Sprite scoreImage;
	public Sprite noScoreImage;
	public int winningNum;

	[SerializeField]
	int[] score = new int[2];

	void Start()
	{
		score = GameManager.instance.score;
		for(int i = 0; i < playerScore.Length; i++)
		{
			playerScore[i].sprite = noScoreImage;
			enemyScore[i].sprite = noScoreImage;
		}
		UpdateScore();
	}

	void OnEnable()
	{
		OnGameOver -= EndGame;
		OnGameOver += EndGame;
	}

	void OnDisable()
	{
		OnGameOver -= EndGame;
	}

	public void AddScore(int scorer)
	{
		score[scorer]++;
		UpdateScore();
		if(score[scorer] == winningNum)
		{
			OnGameOver();
		}
	}

	void UpdateScore()
	{
		for(int i = 0; i < score[0]; i++)
		{
			playerScore[i].sprite = scoreImage;
		}
		for(int i = 0; i < score[1]; i++)
		{
			enemyScore[i].sprite = scoreImage;
		}
	}

	void EndGame()
	{
		if(score[0] == winningNum)
		{
			winText.text = "You Win!";
		}
		else if(score[1] == winningNum)
		{
			winText.text = "You Lose!";
		}
		UpdateScore();
	}
}
