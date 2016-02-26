using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIManager : MonoBehaviour {

    public ScrollRectSnap player1Faces, player2Faces;
	public ScrollRectSnap playerFaces, enemyFaces;
    public Slider p1Health, p2Health, p1Power, p2Power;

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
}
