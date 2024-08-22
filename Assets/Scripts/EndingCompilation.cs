using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class EndingCompilation : MonoBehaviour
{
	public UIDocument _doc;

	private Button _backButton;
	private Button _ending1;
	private Button _ending2;
	private Button _ending3;
	private Button _ending4;
	private Button _ending5;

	private FadeInScript _panelFadeInScript;

	private void Awake()
	{
		_backButton = _doc.rootVisualElement.Q<Button>("BackButton");
		_ending1		= _doc.rootVisualElement.Q<Button>("EndingButton1");
		_ending2		= _doc.rootVisualElement.Q<Button>("EndingButton2");
		_ending3		= _doc.rootVisualElement.Q<Button>("EndingButton3");
		_ending4		= _doc.rootVisualElement.Q<Button>("EndingButton4");
		_ending5		= _doc.rootVisualElement.Q<Button>("EndingButton5");

		_panelFadeInScript = GameObject.Find("GoToEnding Panel").GetComponent<FadeInScript>();

		_backButton.clicked += BackButtonClicked;
		_ending1.clicked		+= Ending1Clicked;
		_ending2.clicked		+= Ending2Clicked;
		_ending3.clicked		+= Ending3Clicked;
		_ending4.clicked		+= Ending4Clicked;
		_ending5.clicked		+= Ending5Clicked;
	}

	private void BackButtonClicked()
	{
		SceneManager.LoadScene("MainMenu");
	}
	private void Ending1Clicked()
	{
		_panelFadeInScript.StartFadeIn();
		Invoke("LoadEnding1", _panelFadeInScript.fadeTime);
	}
	private void Ending2Clicked()
	{
		_panelFadeInScript.StartFadeIn();
		Invoke("LoadEnding2", _panelFadeInScript.fadeTime);
	}
	private void Ending3Clicked()
	{
		_panelFadeInScript.StartFadeIn();
		Invoke("LoadEnding3", _panelFadeInScript.fadeTime);
	}
	private void Ending4Clicked()
	{
		_panelFadeInScript.StartFadeIn();
		Invoke("LoadEnding4", _panelFadeInScript.fadeTime);
	}
	private void Ending5Clicked()
	{
		_panelFadeInScript.StartFadeIn();
		Invoke("LoadEnding5", _panelFadeInScript.fadeTime);
	}

	private void LoadEnding1()
	{
		SceneManager.LoadScene("Ending1");
	}
	private void LoadEnding2()
	{
		SceneManager.LoadScene("Ending2");
	}
	private void LoadEnding3()
	{
		SceneManager.LoadScene("Ending3");
	}
	private void LoadEnding4()
	{
		SceneManager.LoadScene("Ending4");
	}
	private void LoadEnding5()
	{
		SceneManager.LoadScene("Ending5");
	}
}
