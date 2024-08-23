using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OpeningManager : MonoBehaviour
{
	private OpeningTextController _textController;
	private FadeOutScript _textFadeOutScript;

	private CutSceneAudioManager _audioManager;

	private FadeInScript _cut1FadeInScript;
	private FadeInScript _cut2FadeInScript;
	private FadeInScript _cut3FadeInScript;
	private FadeInScript _cut4FadeInScript;
	private FadeInScript _cut5FadeInScript;

	private FadeInScript _panelFadeInScript;

	private FadeOutScript _cut1FadeOutScript;
	private FadeOutScript _cut2FadeOutScript;
	private FadeOutScript _cut3FadeOutScript;
	private FadeOutScript _cut4FadeOutScript;
	private FadeOutScript _cut5FadeOutScript;

	public float holdCut1;
	public float holdCut2;
	public float holdCut3;
	public float holdCut4;
	public float holdCut5;

	public float beforeOpening;
	public float beforeText;

	private void Awake()
	{
		_textController	= GameObject.Find("Opening Text").GetComponent<OpeningTextController>();
		_textFadeOutScript = GameObject.Find("Opening Text").GetComponent<FadeOutScript>();

		_audioManager = GameObject.Find("AudioManager").GetComponent<CutSceneAudioManager>();

		_cut1FadeInScript = GameObject.Find("Cut1").GetComponent<FadeInScript>();
		_cut2FadeInScript = GameObject.Find("Cut2").GetComponent<FadeInScript>();
		_cut3FadeInScript = GameObject.Find("Cut3").GetComponent<FadeInScript>();
		_cut4FadeInScript = GameObject.Find("Cut4").GetComponent<FadeInScript>();
		_cut5FadeInScript = GameObject.Find("Cut5").GetComponent<FadeInScript>();

		_panelFadeInScript = GameObject.Find("GoToGameScene Panel").GetComponent<FadeInScript>();

		_cut1FadeOutScript = GameObject.Find("Cut1").GetComponent<FadeOutScript>();
		_cut2FadeOutScript = GameObject.Find("Cut2").GetComponent<FadeOutScript>();
		_cut3FadeOutScript = GameObject.Find("Cut3").GetComponent<FadeOutScript>();
		_cut4FadeOutScript = GameObject.Find("Cut4").GetComponent<FadeOutScript>();
		_cut5FadeOutScript = GameObject.Find("Cut5").GetComponent<FadeOutScript>();
	}

	void Start()
	{
		StartCoroutine(StartOpening());
	}
	private IEnumerator StartOpening()
	{
		yield return StartCoroutine(DisplayCut1());
		yield return StartCoroutine(DisplayCut2());
		yield return StartCoroutine(DisplayCut3());
		yield return StartCoroutine(DisplayCut4());
		yield return StartCoroutine(DisplayCut5());
		yield return StartCoroutine(GoToGameScene());
	}

	private IEnumerator DisplayCut1()
	{
		_audioManager.StartFadeIn();
		_cut1FadeInScript.StartFadeIn();
		yield return new WaitForSeconds(_cut1FadeInScript.fadeTime + beforeText);
		yield return StartCoroutine(_textController.DisplayText1());
		yield return new WaitForSeconds(holdCut1);
	}

	private IEnumerator DisplayCut2()
	{
		_textFadeOutScript.StartFadeOut();
		_cut1FadeOutScript.StartFadeOut();
		_cut2FadeInScript.StartFadeIn();
		yield return new WaitForSeconds(_cut2FadeInScript.fadeTime + beforeText);
		_textController.ResetText();
		yield return StartCoroutine(_textController.DisplayText2());
		yield return new WaitForSeconds(holdCut2);
	}

	private IEnumerator DisplayCut3()
	{
		_textFadeOutScript.StartFadeOut();
		_cut2FadeOutScript.StartFadeOut();
		_cut3FadeInScript.StartFadeIn();
		yield return new WaitForSeconds(_cut3FadeInScript.fadeTime + beforeText);
		_textController.ResetText();
		yield return StartCoroutine(_textController.DisplayText3());
		yield return new WaitForSeconds(holdCut3);
	}

	private IEnumerator DisplayCut4()
	{
		_textFadeOutScript.StartFadeOut();
		_cut3FadeOutScript.StartFadeOut();
		_cut4FadeInScript.StartFadeIn();
		yield return new WaitForSeconds(_cut4FadeInScript.fadeTime + beforeText);
		_textController.ResetText();
		yield return StartCoroutine(_textController.DisplayText4());
		yield return new WaitForSeconds(holdCut4);
	}

	private IEnumerator DisplayCut5()
	{
		_textFadeOutScript.StartFadeOut();
		_cut4FadeOutScript.StartFadeOut();
		_cut5FadeInScript.StartFadeIn();
		yield return new WaitForSeconds(_cut5FadeInScript.fadeTime + beforeText);
		_textController.ResetText();
		yield return StartCoroutine(_textController.DisplayText5());
		yield return new WaitForSeconds(holdCut5);
	}

	private IEnumerator GoToGameScene()
	{
		_audioManager.StartFadeOut();
		_panelFadeInScript.StartFadeIn();
		yield return new WaitForSeconds(_panelFadeInScript.fadeTime);
		SceneManager.LoadScene("GameScene");
	}
}
