using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIManager : MonoBehaviour {

    public ScrollRectSnap player1Faces, player2Faces;
	public ScrollRectSnap playerFaces, enemyFaces;
    public Slider p1Health, p2Health, p1Power, p2Power;
	public Text timerText, scoreText;
	public GameObject finishPanel;
	public Text finishScoreText;
	public ButtonUtil BU;

	[HideInInspector]
	public float timer, timerResetNum;
	bool hasStarted = true;

    public void LoadArena()
    {
        GameManager.instance.player1Face = player1Faces.currentImg;
        GameManager.instance.player2Face = player2Faces.currentImg;       
        SceneManager.LoadScene("Arena");
    }

	public void LoadSinglePlayer()
	{
		GameManager.instance.player1Face = playerFaces.currentImg;
		GameManager.instance.enemyFace = enemyFaces.currentImg;
		SceneManager.LoadScene("SinglePlayer");
	}

	void UpdateText()
	{
		string minutes = Mathf.Floor(timer / 60).ToString("00");
		string seconds = Mathf.Floor(timer % 60).ToString("00");

		timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
	}

	public void UpdateScore(int[] score)
	{
		scoreText.text = string.Format("{0}-{1}", score[0], score[1]);
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
			timerText.text = "00:00";
			return;
		}
		timer--;
		UpdateText();
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
		finishScoreText.text = string.Format("{0}-{1}", score[0], score[1]);
		finishPanel.SetActive(true);
	}
}
