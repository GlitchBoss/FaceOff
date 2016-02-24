using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIManager : MonoBehaviour {

    public ScrollRectSnap player1Faces, player2Faces;
    public Slider p1Health, p2Health, p1Power, p2Power;

    public void LoadArena()
    {
        GameManager.instance.player1Face = player1Faces.currentImg;
        GameManager.instance.player2Face = player2Faces.currentImg;       
        SceneManager.LoadScene("Arena");
    }
}
