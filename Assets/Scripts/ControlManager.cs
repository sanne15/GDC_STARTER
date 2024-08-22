using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager : MonoBehaviour
{
    public bool isCtrlPressed { get; private set; }
    public bool isLPressed { get; private set; }
    public bool isLogPanelVisible = false;
    public CanvasGroup LogPanel;
    private CustomerManager customerManager;
    public float fadeDuration = 0.3f;

    void Start()
    {
        // CustomerManager 참조 가져오기
        customerManager = FindObjectOfType<CustomerManager>();
        if (customerManager == null)
        {
            Debug.LogError("CustomerManager not found in the scene.");
        }
    }

    void Update()
    {
        // Ctrl 키 입력 상태 추적
        isCtrlPressed = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);

        // Shift + E를 누르면 현재 대화 중인 캐릭터를 강제 퇴장
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.X))
        {
            if (customerManager != null)
            {
                customerManager.ForceExitCurrentCustomer();
            }
        }

        // L 키를 누르면 대화 로그 띄우기
        isLPressed = Input.GetKeyDown(KeyCode.L);
        if (isLPressed)
        {
            if (isLogPanelVisible)
            {
                StartCoroutine(LogFadeOut());
            }
            else
            {
                StartCoroutine(LogFadeIn());
            }
        }
    }

    IEnumerator LogFadeIn()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            LogPanel.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            yield return null;
        }
        LogPanel.alpha = 1f;
        isLogPanelVisible = true;
    }

    IEnumerator LogFadeOut()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            LogPanel.alpha = Mathf.Clamp01(1f - (elapsedTime / fadeDuration));
            yield return null;
        }
        LogPanel.alpha = 0f;
        isLogPanelVisible = false;
    }
}
