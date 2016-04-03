using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Data;

public class ScrollRectSnap : MonoBehaviour {

    public RectTransform content;
    public List<Image> bttns = new List<Image>();
    public RectTransform center;
	public FaceData faces;
	public RectTransform parent;
	public Image bttnPrefab;
	public Sprite commingSoon;
	public bool ai;
	public bool playable;

    public int currentImg
    {
        get { return minBttnNum; }
    }

    float[] distance;
    float[] distReposition;
    bool dragging = false;
    int minBttnNum;
    int bttnLength;
	int nextPos;
	float wrapPos;

	[SerializeField]
    int buttonDist;

    void Start()
    {
		//unlocked = PlayerPrefs.GetInt("AIUnlocked", 1);
		nextPos = 0;

		for (int i = 0; i < faces.faces.Count; i++)
		{
			Image face = Instantiate(bttnPrefab);
			face.rectTransform.SetParent(parent, false);
			face.rectTransform.anchoredPosition = new Vector2(face.rectTransform.anchoredPosition.x, face.rectTransform.anchoredPosition.y + nextPos);
			face.sprite = faces.faces[i].image;
			if (faces.faces[i].unlocked)
			{
				face.transform.FindChild("Locked").gameObject.SetActive(false);
			}
			bttns.Add(face);
			nextPos += buttonDist;
		}

		//if (ai)
		//{
		//	for(int i = 0; i < unlocked; i++)
		//	{
		//		bttns[i].transform.FindChild("Locked").gameObject.SetActive(false);
		//	}
		//}
		//else
		//{
		//	for (int i = 0; i < bttns.Count; i++)
		//	{
		//		bttns[i].transform.FindChild("Locked").gameObject.SetActive(false);
		//		unlocked = bttns.Count;
		//	}
		//}
		

		Image cs = Instantiate(bttnPrefab);
		cs.rectTransform.SetParent(parent, false);
		cs.rectTransform.anchoredPosition = new Vector2(cs.rectTransform.anchoredPosition.x, cs.rectTransform.anchoredPosition.y + nextPos);
		cs.sprite = commingSoon;
		cs.transform.FindChild("Locked").gameObject.SetActive(false);
		bttns.Add(cs);
		nextPos += buttonDist;

		wrapPos = nextPos - (buttonDist * 2f);

        bttnLength = bttns.Count;
        distance = new float[bttnLength];
        distReposition = new float[bttnLength];

        buttonDist = (int)Mathf.Abs(bttns[1].GetComponent<RectTransform>().anchoredPosition.y - 
            bttns[0].GetComponent<RectTransform>().anchoredPosition.y);
    }

    void Update()
    {
        for(int i = 0; i < bttns.Count; i++)
        {
            distReposition[i] = center.GetComponent<RectTransform>().position.y -
                bttns[i].GetComponent<RectTransform>().position.y;
            distance[i] = Mathf.Abs(distReposition[i]);

            if(distReposition[i] > wrapPos)
            {
                float curX = bttns[i].GetComponent<RectTransform>().anchoredPosition.x;
                float curY = bttns[i].GetComponent<RectTransform>().anchoredPosition.y;

                Vector2 newAnchoredPos = new Vector2(curX, curY + (bttnLength * buttonDist));
                bttns[i].GetComponent<RectTransform>().anchoredPosition = newAnchoredPos;
            }
            if (distReposition[i] < -wrapPos)
            {
                float curX = bttns[i].GetComponent<RectTransform>().anchoredPosition.x;
                float curY = bttns[i].GetComponent<RectTransform>().anchoredPosition.y;

                Vector2 newAnchoredPos = new Vector2(curX, curY - (bttnLength * buttonDist));
                bttns[i].GetComponent<RectTransform>().anchoredPosition = newAnchoredPos;
            }
        }

        float minDist = Mathf.Min(distance);
        for(int a = 0; a < bttns.Count; a++)
        {
            if(minDist == distance[a])
            {
                minBttnNum = a;
            }
        }

        if (!dragging)
        {
            LerpToButton(-bttns[minBttnNum].GetComponent<RectTransform>().anchoredPosition.y);
        }
		if (!ai)
		{
			playable = true;
			if (minBttnNum == bttns.Count - 1)
				playable = false;
			return;
		}

		if(minBttnNum == bttns.Count - 1)
		{
			playable = false;
			return;
		}
		playable = faces.faces[minBttnNum].unlocked;

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
