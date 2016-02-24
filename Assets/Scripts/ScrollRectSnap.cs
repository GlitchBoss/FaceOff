using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScrollRectSnap : MonoBehaviour {

    public RectTransform content;
    public Image[] bttns;
    public RectTransform center;
    public int currentImg
    {
        get { return minBttnNum; }
    }

    float[] distance;
    float[] distReposition;
    bool dragging = false;
    int buttonDist;
    int minBttnNum;
    int bttnLength;

    void Start()
    {
        bttnLength = bttns.Length;
        distance = new float[bttnLength];
        distReposition = new float[bttnLength];

        buttonDist = (int)Mathf.Abs(bttns[1].GetComponent<RectTransform>().anchoredPosition.y - 
            bttns[0].GetComponent<RectTransform>().anchoredPosition.y);
    }

    void Update()
    {
        for(int i = 0; i < bttns.Length; i++)
        {
            distReposition[i] = center.GetComponent<RectTransform>().position.y -
                bttns[i].GetComponent<RectTransform>().position.y;
            distance[i] = Mathf.Abs(distReposition[i]);

            if(distReposition[i] > 900)
            {
                float curX = bttns[i].GetComponent<RectTransform>().anchoredPosition.x;
                float curY = bttns[i].GetComponent<RectTransform>().anchoredPosition.y;

                Vector2 newAnchoredPos = new Vector2(curX, curY + (bttnLength * buttonDist));
                bttns[i].GetComponent<RectTransform>().anchoredPosition = newAnchoredPos;
            }
            if (distReposition[i] < -900)
            {
                float curX = bttns[i].GetComponent<RectTransform>().anchoredPosition.x;
                float curY = bttns[i].GetComponent<RectTransform>().anchoredPosition.y;

                Vector2 newAnchoredPos = new Vector2(curX, curY - (bttnLength * buttonDist));
                bttns[i].GetComponent<RectTransform>().anchoredPosition = newAnchoredPos;
            }
        }

        float minDist = Mathf.Min(distance);
        for(int a = 0; a < bttns.Length; a++)
        {
            if(minDist == distance[a])
            {
                minBttnNum = a;
            }
        }

        if (!dragging)
        {
            //LerpToButton(minBttnNum * -buttonDist);
            LerpToButton(-bttns[minBttnNum].GetComponent<RectTransform>().anchoredPosition.y);
        }
    }

    void LerpToButton(float pos)
    {
        float newY = Mathf.Lerp(content.anchoredPosition.y, pos, Time.deltaTime * 10f);
        Vector2 newPos = new Vector2(content.anchoredPosition.x, newY);

        content.anchoredPosition = newPos;
    }

    public void StartDrag()
    {
        dragging = true;
    }

    public void EndDrag()
    {
        dragging = false;
    }
}
