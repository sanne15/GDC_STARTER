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

    public CustomerManager customerManager;

    private int originalFadePanelIndex;
    private int originalDayTextIndex;

    private Moneymanager moneyManager;

    void Start()
    {
        UpdateDayText();

         moneyManager = FindObjectOfType<Moneymanager>();

        if (moneyManager == null)
        {
            Debug.LogError("MoneyManager not existing in scene.");
            // return;
        }

        originalFadePanelIndex = fadePanel.transform.GetSiblingIndex();
        originalDayTextIndex = dayText.transform.GetSiblingIndex();

        StartDay();
    }

    void StartDay()
    {
        customerManager.InitializeDay(currentDay);
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
        // Fadeout
        yield return StartCoroutine(FadeOut());

        // Update Date
        UpdateDayText();

        // SFX
        //AudioSource.PlayClipAtPoint(effectSound, transform.position);

        // Chuou
        dayText.rectTransform.anchoredPosition = new Vector2(-3.0f, 0);
        dayText.rectTransform.pivot = new Vector2(0.5f, 0.5f);
        dayText.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        dayText.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        dayText.transform.localScale = new Vector3(4, 4, 1);
        dayText.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(2.0f);

        // Text transition
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

        // money Added
        moneyManager.AddMoney(0);

        // Fadein
        yield return StartCoroutine(FadeIn());

        StartDay();
    }

    IEnumerator FadeOut()
    {
        CanvasGroup canvasGroup = fadePanel.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("CanvasGroup is missing on fadePanel.");
            yield break;
        }

        // fadePanel& dayText go to the first plane
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

        // Fade end >> return to appropriate plane
        fadePanel.transform.SetSiblingIndex(originalFadePanelIndex);
        dayText.transform.SetSiblingIndex(originalDayTextIndex);
    }
}
