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
        // CustomerManager ���� ��������
        customerManager = FindObjectOfType<CustomerManager>();
        if (customerManager == null)
        {
            Debug.LogError("CustomerManager not found in the scene.");
        }
    }

    void Update()
    {
        // Ctrl Ű �Է� ���� ����
        isCtrlPressed = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);

        // Shift + E�� ������ ���� ��ȭ ���� ĳ���͸� ���� ����
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.X))
        {
            if (customerManager != null)
            {
                customerManager.ForceExitCurrentCustomer();
            }
        }

        // L Ű�� ������ ��ȭ �α� ����
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
