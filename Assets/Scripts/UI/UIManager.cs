using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIManager : MonoBehaviour {

	[Tooltip("0=player1Faces, 1=player2Faces, 2=playerFaces, 3=enemyFaces")]
	public ScrollRectSnap[] scrollSnaps;
	[Tooltip("0=p1Health, 1=p2Health, 2=p1Power, 3=p2Power")]
	public Slider[] sliders;
	[Tooltip("0=timerText, 1=scoreText, 2=finishScoreText")]
	public Text[] text;
	public GameObject finishPanel;
	public ButtonUtil BU;

	[HideInInspector]
	public float timer, timerResetNum;

	bool hasStarted = true;

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

	void UpdateText()
	{
		string minutes = Mathf.Floor(timer / 60).ToString("00");
		string seconds = Mathf.Floor(timer % 60).ToString("00");

		text[0].text = string.Format("{0:00}:{1:00}", minutes, seconds);
	}

	public void UpdateScore(int[] score)
	{
		text[1].text = string.Format("{0}-{1}", score[0], score[1]);
	}

	public void StartTimer(float time)
	{
		timer = time;
		timerResetNum = timer;
		UpdateText();
		InvokeRepeating("DecreaseTimeRemaining", 1.0f, 1.0f);
		hasStarted = true;
	}

	void DecreaseTimeRemaining()
	{
		if (timer <= 0)
		{
			StopTimer();
			text[0].text = "00:00";
			return;
		}
		timer--;
		UpdateText();
	}

	public void Move(int value)
	{
		GameManager.instance.players[0].Move(value);
	}

	public void StopTimer()
	{
		if (!hasStarted)
			return;
		CancelInvoke("DecreaseTimeRemaining");
		//CancelInvoke("Flash");
		hasStarted = false;
		timer = timerResetNum;
		GameOver();
	}

	public void GameOver()
	{
		BU.PauseGame();
		int[] score = GameManager.instance.score;
		text[2].text = string.Format("{0}-{1}", score[0], score[1]);
		finishPanel.SetActive(true);
	}
}
