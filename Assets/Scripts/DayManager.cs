using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DayManager : MonoBehaviour
{
    public int currentDay = 1;
    public TMP_Text dayText;
    public GameObject fadePanel;
    public GameObject panel; // sibling index confirmation

    private int originalFadePanelIndex;
    private int originalDayTextIndex;

    void Start()
    {
        UpdateDayText();

        originalFadePanelIndex = fadePanel.transform.GetSiblingIndex();
        originalDayTextIndex = dayText.transform.GetSiblingIndex();
    }

    public void NextDay()
    {
        currentDay++;
        StartCoroutine(DayTransition());
    }

    void UpdateDayText()
    {
        dayText.text = currentDay + "일차";
    }

    IEnumerator DayTransition()
    {
        // 페이드 아웃
        yield return StartCoroutine(FadeOut());

        // 날짜 업데이트
        UpdateDayText();

        // 텍스트 중앙에 표시
        dayText.rectTransform.anchoredPosition = new Vector2(-3.0f, 0);
        dayText.rectTransform.pivot = new Vector2(0.5f, 0.5f);
        dayText.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        dayText.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        dayText.transform.localScale = new Vector3(4, 4, 1);
        dayText.gameObject.SetActive(true);

        // 효과음 재생 (효과음 파일 추가 필요)
        //AudioSource.PlayClipAtPoint(effectSound, transform.position);

        // 텍스트 중앙에 1초간 대기
        yield return new WaitForSeconds(2.0f);

        // 텍스트 이동
        float duration = 2.0f;
        Vector2 startPosition = dayText.rectTransform.anchoredPosition;
        Vector2 endPosition = new Vector2(860, 500);
        Vector3 startScale = new Vector3(4, 4, 4);
        Vector3 endScale = new Vector3(1, 1, 1);
        float elapsed = 0;

        while (elapsed < duration)
        {
            dayText.rectTransform.anchoredPosition = Vector2.Lerp(startPosition, endPosition, elapsed / duration);
            dayText.transform.localScale = Vector3.Lerp(startScale, endScale, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        dayText.rectTransform.anchoredPosition = endPosition;
        dayText.transform.localScale = endScale;

        // 페이드 인
        yield return StartCoroutine(FadeIn());

        //dayText.gameObject.SetActive(false);
    }

    IEnumerator FadeOut()
    {
        CanvasGroup canvasGroup = fadePanel.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("CanvasGroup is missing on fadePanel.");
            yield break;
        }

        // fadePanel과 dayText를 맨 위로 이동
        fadePanel.transform.SetAsLastSibling();
        dayText.transform.SetAsLastSibling();

        float duration = 0.5f;
        float elapsed = 0;

        while (elapsed < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1;
    }

    IEnumerator FadeIn()
    {
        CanvasGroup canvasGroup = fadePanel.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("CanvasGroup is missing on fadePanel.");
            yield break;
        }

        float duration = 0.5f;
        float elapsed = 0;

        while (elapsed < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(1, 0, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 0;

        // 페이드 인이 완료되면 원래 위치로 돌아옴
        fadePanel.transform.SetSiblingIndex(originalFadePanelIndex);
        dayText.transform.SetSiblingIndex(originalDayTextIndex);
    }
}
