using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeamLogoManager : MonoBehaviour
{
	private FadeInScript	_fadeInScript;
	private FadeOutScript _fadeOutScript;
	public	float					holdTime = 1f;

	private void Awake()
	{
		_fadeInScript		= GameObject.Find("Logo").GetComponent<FadeInScript>();
		_fadeOutScript	= GameObject.Find("Logo").GetComponent<FadeOutScript>();
	}

	void Start()
  {
		_fadeInScript.StartFadeIn();
		holdTime += _fadeInScript.fadeTime;
		Invoke("DoFadeOut", holdTime);
		holdTime += _fadeOutScript.fadeTime;
		Invoke("GoToMainMenu", holdTime);
	}

	private void DoFadeOut()
	{
		_fadeOutScript.StartFadeOut();
	}

	private void GoToMainMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}
}
