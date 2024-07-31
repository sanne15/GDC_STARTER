using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IDragHandler
{
    public Canvas canvas;

    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData) // �ùٸ� �޼��� �̸��� ������
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
}
