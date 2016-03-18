using UnityEngine;
using UnityEngine.SceneManagement;
using GLITCH.Utilities;

public class ButtonUtil : MonoBehaviour {

    public UIManager UIM;

	//public void StartGame()
	//{
	//	GameManager.instance.StartGame ();
	//}

    public void PauseGame()
    {
		ButtonUtility.Pause_Game();
	}

    public void ResumeGame()
    {
		ButtonUtility.Resume_Game();
	}

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

	public void Reset()
	{
		GameManager.instance.Reset();
	}

    public void LoadLevel(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void LoadArena()
    {
        UIM.LoadArena();
    }

	public void LoadSinglePlayer()
	{
		UIM.LoadSinglePlayer();
	}
}
