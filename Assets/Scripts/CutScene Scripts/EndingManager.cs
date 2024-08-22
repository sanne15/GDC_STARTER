using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingManager : MonoBehaviour
{
	private EndingTextController _textController;

	private FadeInScript _cutFadeInScript;

	private FadeInScript _panelFadeInScript;

	public float holdText1;
	public float holdText2;
	public float holdText3;
	public float holdText4;
	public float holdText5;
	public float holdText6;

	public float beforeText;

	private void Awake()
	{
		_textController = GameObject.Find("Ending Text").GetComponent<EndingTextController>();

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
		yield return StartCoroutine(GoToMainMenu());
	}

	private IEnumerator DisplayCut1()
	{
		_cutFadeInScript.StartFadeIn();
		yield return new WaitForSeconds(_cutFadeInScript.fadeTime + beforeText);
		yield return StartCoroutine(_textController.DisplayText1());
		yield return new WaitForSeconds(holdText1);
	}

	private IEnumerator DisplayCut2()
	{
		_textController.ResetText();
		yield return new WaitForSeconds(beforeText);
		yield return StartCoroutine(_textController.DisplayText2());
		yield return new WaitForSeconds(holdText2);
	}

	private IEnumerator DisplayCut3()
	{
		_textController.ResetText();
		yield return new WaitForSeconds(beforeText);
		yield return StartCoroutine(_textController.DisplayText3());
		yield return new WaitForSeconds(holdText3);
	}

	private IEnumerator DisplayCut4()
	{
		_textController.ResetText();
		yield return new WaitForSeconds(beforeText);
		yield return StartCoroutine(_textController.DisplayText4());
		yield return new WaitForSeconds(holdText4);
	}

	private IEnumerator DisplayCut5()
	{
		_textController.ResetText();
		yield return new WaitForSeconds(beforeText);
		yield return StartCoroutine(_textController.DisplayText5());
		yield return new WaitForSeconds(holdText5);
	}

	private IEnumerator GoToMainMenu()
	{
		_panelFadeInScript.StartFadeIn();
		yield return new WaitForSeconds(_panelFadeInScript.fadeTime);
		SceneManager.LoadScene("MainMenu");
	}
}
