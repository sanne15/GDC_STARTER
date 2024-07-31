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

    public void OnDrag(PointerEventData eventData) // 올바른 메서드 이름과 접근자
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
}
