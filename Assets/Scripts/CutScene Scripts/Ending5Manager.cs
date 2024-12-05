using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending5Manager : MonoBehaviour
{
	private Ending5TextController _textController;
	private FadeOutScript _textFadeOutScript;

	private CutSceneAudioManager _audioManager;

	private FadeInScript _cutFadeInScript;

	private FadeInScript _panelFadeInScript;

	public float holdText1;
	public float holdText2;
	public float holdText3;
	public float holdText4;
	public float holdText5;
	public float holdText6;
	public float holdText7;
	public float holdText8;

	public float beforeText;

	private void Awake()
	{
		_textController = GameObject.Find("Ending Text").GetComponent<Ending5TextController>();
		_textFadeOutScript = GameObject.Find("Ending Text").GetComponent<FadeOutScript>();

		_audioManager = GameObject.Find("AudioManager").GetComponent<CutSceneAudioManager>();

		_cutFadeInScript = GameObject.Find("Cut").GetComponent<FadeInScript>();

		_panelFadeInScript = GameObject.Find("GoToMainMenu Panel").GetComponent<FadeInScript>();
	}

	void Start()
	{
		StartCoroutine(StartEnding());
	}

	private IEnumerator StartEnding()
	{
		yield return StartCoroutine(DisplayCut1());
		yield return StartCoroutine(DisplayCut2());
		yield return StartCoroutine(DisplayCut3());
		yield return StartCoroutine(DisplayCut4());
		yield return StartCoroutine(DisplayCut5());
		yield return StartCoroutine(DisplayCut6());
		yield return StartCoroutine(DisplayCut7());
		yield return StartCoroutine(DisplayCut8());
		yield return StartCoroutine(GoToMainMenu());
	}

	private IEnumerator DisplayCut1()
	{
		_audioManager.StartFadeIn();
		_cutFadeInScript.StartFadeIn();
		yield return new WaitForSeconds(_cutFadeInScript.fadeTime + beforeText);
		yield return StartCoroutine(_textController.DisplayText1());
		yield return new WaitForSeconds(holdText1);
	}

	private IEnumerator DisplayCut2()
	{
		_textFadeOutScript.StartFadeOut();
		yield return new WaitForSeconds(_textFadeOutScript.fadeTime + beforeText);
		_textController.ResetText();
		yield return StartCoroutine(_textController.DisplayText2());
		yield return new WaitForSeconds(holdText2);
	}

	private IEnumerator DisplayCut3()
	{
		_textFadeOutScript.StartFadeOut();
		yield return new WaitForSeconds(_textFadeOutScript.fadeTime + beforeText);
		_textController.ResetText();
		yield return StartCoroutine(_textController.DisplayText3());
		yield return new WaitForSeconds(holdText3);
	}

	private IEnumerator DisplayCut4()
	{
		_textFadeOutScript.StartFadeOut();
		yield return new WaitForSeconds(_textFadeOutScript.fadeTime + beforeText);
		_textController.ResetText();
		yield return StartCoroutine(_textController.DisplayText4());
		yield return new WaitForSeconds(holdText4);
	}

	private IEnumerator DisplayCut5()
	{
		_textFadeOutScript.StartFadeOut();
		yield return new WaitForSeconds(_textFadeOutScript.fadeTime + beforeText);
		_textController.ResetText();
		yield return StartCoroutine(_textController.DisplayText5());
		yield return new WaitForSeconds(holdText5);
	}

	private IEnumerator DisplayCut6()
	{
		_textFadeOutScript.StartFadeOut();
		yield return new WaitForSeconds(_textFadeOutScript.fadeTime + beforeText);
		_textController.ResetText();
		yield return StartCoroutine(_textController.DisplayText6());
		yield return new WaitForSeconds(holdText6);
	}

	private IEnumerator DisplayCut7()
	{
		_textFadeOutScript.StartFadeOut();
		yield return new WaitForSeconds(_textFadeOutScript.fadeTime + beforeText);
		_textController.ResetText();
		yield return StartCoroutine(_textController.DisplayText7());
		yield return new WaitForSeconds(holdText7);
	}

	private IEnumerator DisplayCut8()
	{
		_textFadeOutScript.StartFadeOut();
		yield return new WaitForSeconds(_textFadeOutScript.fadeTime + beforeText);
		_textController.ResetText();
		yield return StartCoroutine(_textController.DisplayText8());
		yield return new WaitForSeconds(holdText8);
	}

	private IEnumerator GoToMainMenu()
	{
		_audioManager.StartFadeOut();
		_panelFadeInScript.StartFadeIn();
		yield return new WaitForSeconds(_panelFadeInScript.fadeTime);
		SceneManager.LoadScene("MainMenu");
	}
}
