using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DayManager : MonoBehaviour
{
    public enum GameState
    {
        Dialogue,    // 대화 중
        Settlement,  // 정산 중
        Other        // 다른 상태 추가 가능
    }
    public GameState currentState = GameState.Dialogue; // 현재 상태를 관리하는 변수

    public int currentDay = 1;
    public TMP_Text dayText;
    public GameObject fadePanel;
    public GameObject panel; // sibling index confirmation

    public CustomerManager customerManager;
    public GameObject earningsPanel; // 정산 패널 UI
    public TMP_Text earningsText; // 수익 텍스트
    public TMP_Text penaltyText; // 벌금 텍스트
    public TMP_Text netEarningsText; // 순수익 텍스트
    public Button nextDayButton; // Next Day 버튼

    private int originalFadePanelIndex;
    private int originalDayTextIndex;

    private bool nextDayButtonClicked = false; // 버튼 클릭 여부를 추적하는 변수

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

        nextDayButton.onClick.AddListener(OnNextDayButtonClicked);

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
        nextDayButtonClicked = false;
    }

    public int GetCurrentDay()
    {
        return currentDay;
    }

    void UpdateDayText()
    {
        dayText.text = currentDay + "일차";
    }

    IEnumerator DayTransition()
    {
        // Fadeout
        yield return StartCoroutine(FadeOut());

        // SFX
        //AudioSource.PlayClipAtPoint(effectSound, transform.position);

        // Chuou
        dayText.rectTransform.anchoredPosition = new Vector2(0, -50);
        dayText.rectTransform.pivot = new Vector2(0.5f, 1.0f);
        dayText.rectTransform.anchorMin = new Vector2(0.5f, 1.0f);
        dayText.rectTransform.anchorMax = new Vector2(0.5f, 1.0f);
        dayText.transform.localScale = new Vector3(2, 2, 2);
        dayText.gameObject.SetActive(true);

        ShowEarningsPanel();

        yield return new WaitUntil(() => nextDayButtonClicked);

        // Update Date
        UpdateDayText();

        // Text transition
        float duration = 1.0f;
        Vector2 startPosition = dayText.rectTransform.anchoredPosition;
        Vector2 endPosition = new Vector2(860, -12);
        Vector3 startScale = new Vector3(2, 2, 2);
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


        // Fadein
        yield return StartCoroutine(FadeIn());

        currentState = GameState.Dialogue;

        StartDay();
    }

    void ShowEarningsPanel()
    {
        // 예시 UI 설정
        earningsPanel.SetActive(true);
        currentState = GameState.Settlement;

        // 손님 수 계산
        int customersToday = customerManager.GetCustomersToday();
        int earnings = customersToday * 500;
        int penalty = 270000;  // 벌금 계산 로직이 필요함
        int netEarnings = earnings;

        earningsText.text = $"오늘의 수익: {earnings}₩";
        penaltyText.text = $"이번주의 벌금: {penalty}₩";
        netEarningsText.text = $"총자산: {netEarnings + moneyManager.GetMoney()}₩";

        // money Added
        moneyManager.AddMoney(netEarnings);
    }

    public void OnNextDayButtonClicked()
    {
        nextDayButtonClicked = true;
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

        earningsPanel.SetActive(false);
    }
}
