using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInScript : MonoBehaviour
{
  private CanvasGroup _canvasGroup;
  public float fadeTime;
  float accumTime = 0f;
  private Coroutine fadeCor;

  private void Awake()
  {
    _canvasGroup = gameObject.GetComponent<CanvasGroup>();
  }

  public void StartFadeIn()
  {
		if (fadeCor != null)
    {
      StopAllCoroutines();
      fadeCor = null;
    }
    fadeCor = StartCoroutine(FadeIn());
  }

  private IEnumerator FadeIn()
  {
		accumTime = 0f;
    while (accumTime < fadeTime)
    {
      _canvasGroup.alpha = Mathf.Lerp(0f, 1f, accumTime / fadeTime);
      yield return 0;
      accumTime += Time.deltaTime;
    }
    _canvasGroup.alpha = 1f;
  }
}