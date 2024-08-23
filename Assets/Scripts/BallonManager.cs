using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BallonManager : MonoBehaviour
{
    public Sprite[] sprites;
    public RectTransform[] ichi;
    public TextMeshProUGUI[] textMeshPros; // ��ǳ�� ���� TextMeshProUGUI ������Ʈ��
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
            tmp.text = ""; // ��� TextMeshPro ������Ʈ�� �ؽ�Ʈ�� �� ���ڿ��� �ʱ�ȭ
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
                    if (child != thoughtPanel.rectTransform) // �ڽ�(thoughtPanel)�� ����
                    {
                        child.anchoredPosition -= new Vector2(0, 30); // 40px ���������� ����
                    }
                }
            }            
        }
        else
        {
            onetime = false;

            /// sprite�� recttransform�� index 1���� ���� (����)
            thoughtPanel.sprite = sprites[1];
            thoughtPanel.rectTransform.anchoredPosition = ichi[1].anchoredPosition;
            thoughtPanel.rectTransform.sizeDelta = ichi[1].sizeDelta;

            if(yep ^ lasttime)
            {
                // thoughtPanel ������ ��� GameObject ��ġ�� 40px �ø���
                foreach (RectTransform child in thoughtPanel.GetComponentsInChildren<RectTransform>())
                {
                    if (child != thoughtPanel.rectTransform) // �ڽ�(thoughtPanel)�� ����
                    {
                        child.anchoredPosition += new Vector2(0, 30);
                    }
                }
            }            
        }

        lasttime = yep;
    }
}
