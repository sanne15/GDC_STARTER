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
        dayText.text = currentDay + "����";
    }

    IEnumerator DayTransition()
    {
        // ���̵� �ƿ�
        yield return StartCoroutine(FadeOut());

        // ��¥ ������Ʈ
        UpdateDayText();

        // �ؽ�Ʈ �߾ӿ� ǥ��
        dayText.rectTransform.anchoredPosition = new Vector2(-3.0f, 0);
        dayText.rectTransform.pivot = new Vector2(0.5f, 0.5f);
        dayText.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        dayText.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        dayText.transform.localScale = new Vector3(4, 4, 1);
        dayText.gameObject.SetActive(true);

        // ȿ���� ��� (ȿ���� ���� �߰� �ʿ�)
        //AudioSource.PlayClipAtPoint(effectSound, transform.position);

        // �ؽ�Ʈ �߾ӿ� 1�ʰ� ���
        yield return new WaitForSeconds(2.0f);

        // �ؽ�Ʈ �̵�
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

        // ���̵� ��
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

        // fadePanel�� dayText�� �� ���� �̵�
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

        // ���̵� ���� �Ϸ�Ǹ� ���� ��ġ�� ���ƿ�
        fadePanel.transform.SetSiblingIndex(originalFadePanelIndex);
        dayText.transform.SetSiblingIndex(originalDayTextIndex);
    }
}
