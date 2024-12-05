using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BallonManager : MonoBehaviour
{
    public Sprite[] sprites;
    public RectTransform[] ichi;
    public TextMeshProUGUI[] textMeshPros; // 말풍선 내의 TextMeshProUGUI 컴포넌트들
    public Image thoughtPanel;

    public bool onetime;
    public bool lasttime;

    void Awake()
    {
        onetime = true;
        thoughtPanel = GetComponent<UnityEngine.UI.Image>();
    }
    public void textrefresh()
    {
        foreach (TextMeshProUGUI tmp in textMeshPros)
        {
            tmp.text = ""; // 모든 TextMeshPro 컴포넌트의 텍스트를 빈 문자열로 초기화
        }
    }

    public void IsTalkingtoYou(bool yep)
    {
        if(!yep)
        {
            thoughtPanel.sprite = sprites[0];
            thoughtPanel.rectTransform.anchoredPosition = ichi[0].anchoredPosition;
            thoughtPanel.rectTransform.sizeDelta = ichi[0].sizeDelta;

            if(!onetime && yep ^ lasttime)
            {
                foreach (RectTransform child in thoughtPanel.GetComponentsInChildren<RectTransform>())
                {
                    if (child != thoughtPanel.rectTransform) // 자신(thoughtPanel)은 제외
                    {
                        child.anchoredPosition -= new Vector2(0, 30); // 40px 내려가도록 복원
                    }
                }
            }            
        }
        else
        {
            onetime = false;

            /// sprite와 recttransform은 index 1으로 변경 (상대방)
            thoughtPanel.sprite = sprites[1];
            thoughtPanel.rectTransform.anchoredPosition = ichi[1].anchoredPosition;
            thoughtPanel.rectTransform.sizeDelta = ichi[1].sizeDelta;

            if(yep ^ lasttime)
            {
                // thoughtPanel 하위의 모든 GameObject 위치를 40px 올리기
                foreach (RectTransform child in thoughtPanel.GetComponentsInChildren<RectTransform>())
                {
                    if (child != thoughtPanel.rectTransform) // 자신(thoughtPanel)은 제외
                    {
                        child.anchoredPosition += new Vector2(0, 30);
                    }
                }
            }            
        }

        lasttime = yep;
    }
}
