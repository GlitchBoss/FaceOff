﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using GLITCH.Helpers;

public class UIManager : MonoBehaviour {

	[Tooltip("0=player1Faces, 1=player2Faces, 2=playerFaces, 3=enemyFaces")]
	public ScrollRectSnap[] scrollSnaps;
	public Button play;
	[Tooltip("0=p1Health, 1=p2Health, 2=p1Power, 3=p2Power")]
	public Slider[] sliders;
	public GameObject finishPanel;
	public GameObject tieBreakerText;
	public ButtonUtil BU;
	public Timer timer;
	public GameObject SpecialPowerBtn;

	[HideInInspector]
	public float timerF, timerResetNum;

	bool hasStarted = true;

	void FixedUpdate()
	{
		if (!scrollSnaps[2])
			return;
		if(scrollSnaps[2].playable && scrollSnaps[3].playable)
		{
			play.interactable = true;
		}
		else
		{
			play.interactable = false;
		}
	}

    public void LoadArena()
    {
        GameManager.instance.player1Face = scrollSnaps[0].currentImg;
        GameManager.instance.player2Face = scrollSnaps[1].currentImg;       
        SceneManager.LoadScene("Arena");
    }

	public void LoadSinglePlayer()
	{
		GameManager.instance.player1Face = scrollSnaps[2].currentImg;
		GameManager.instance.enemyFace = scrollSnaps[3].currentImg;
		SceneManager.LoadScene("SinglePlayer");
	}

	public void StartTimer(float time)
	{
		Timer.OnTimerStopped -= EndGame;
		Timer.OnTimerStopped += EndGame;
		timer.StartTimer(time);
		hasStarted = true;
	}

	public void Move(int value)
	{
		GameManager.instance.players[0].Move(value);
	}

	public void StopTimer()
	{
		if (!hasStarted)
			return;
		hasStarted = false;
		if (GameManager.instance.score[0] == GameManager.instance.score[1])
		{
			GameManager.instance.tieBreaker = true;
			tieBreakerText.SetActive(true);
		}
		else
		{
			EndGame();
		}
		Timer.OnTimerStopped -= EndGame;
	}

	public void EndGame()
	{
		BU.PauseGame();
		int[] score = GameManager.instance.score;
		if (score[0] > score[1])
		{
			int enemy = GameManager.instance.enemyFace;
			CheckUnlock(enemy);
		}
		finishPanel.SetActive(true);
	}

	void CheckUnlock(int enemy)
	{
		int unlocked = PlayerPrefs.GetInt("AIUnlocked", 1);
		if(enemy == unlocked - 1)
		{
			unlocked++;
		}
		PlayerPrefs.SetInt("AIUnlocked", unlocked);
	}
}
