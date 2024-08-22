using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutScript : MonoBehaviour
{
	private CanvasGroup _canvasGroup;
	public float fadeTime;
	float accumTime = 0f;
	private Coroutine fadeCor;

	private void Awake()
	{
		_canvasGroup = gameObject.GetComponent<CanvasGroup>();
	}

	public void StartFadeOut()
	{
		if (fadeCor != null)
		{
			StopAllCoroutines();
			fadeCor = null;
		}
		fadeCor = StartCoroutine(FadeOut());
	}

	private IEnumerator FadeOut()
	{
		accumTime = 0f;
		while (accumTime < fadeTime)
		{
			_canvasGroup.alpha = Mathf.Lerp(1f, 0f, accumTime / fadeTime);
			yield return 0;
			accumTime += Time.deltaTime;
		}
		_canvasGroup.alpha = 0f;
	}
}
