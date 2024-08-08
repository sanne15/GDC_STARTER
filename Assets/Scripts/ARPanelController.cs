using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARPanelController : MonoBehaviour
{
    public CanvasGroup arPanel;
    public float fadeDuration = 0.5f;
    private bool isPanelVisible = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isPanelVisible)
            {
                StartCoroutine(FadeOut());
            }
            else
            {
                StartCoroutine(FadeIn());
            }
        }
    }
    
    IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            arPanel.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            yield return null;
        }
        arPanel.alpha = 1f;
        isPanelVisible = true;
    }

    IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            arPanel.alpha = Mathf.Clamp01(1f - (elapsedTime / fadeDuration));
            yield return null;
        }
        arPanel.alpha = 0f;
        isPanelVisible = false;
    }


}
