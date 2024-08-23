using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneAudioManager : MonoBehaviour
{
	private AudioSource _audioSource;
	public float fadeInTime;
	public float fadeOutTime;
	float accumTime = 0f;
	private Coroutine fadeCor;

	private void Awake()
	{
		_audioSource = GetComponent<AudioSource>();
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
		while (accumTime < fadeInTime)
		{
			_audioSource.volume = Mathf.Lerp(0f, 1f, accumTime / fadeInTime);
			yield return 0;
			accumTime += Time.deltaTime;
		}
		_audioSource.volume = 1f;
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
		while (accumTime < fadeOutTime)
		{
			_audioSource.volume = Mathf.Lerp(1f, 0f, accumTime / fadeOutTime);
			yield return 0;
			accumTime += Time.deltaTime;
		}
		_audioSource.volume = 0f;
	}
}
