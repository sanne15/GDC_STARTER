using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class EndingCompilation : MonoBehaviour
{
	private UIDocument _doc;
	private Button _backButton;
	private Button _ending1;
	private Button _ending2;
	private Button _ending3;
	private Button _ending4;
	private Button _ending5;

	private void Awake()
	{
		_doc = GetComponent<UIDocument>();

		_backButton = _doc.rootVisualElement.Q<Button>("BackButton");
		_ending1		= _doc.rootVisualElement.Q<Button>("EndingButton1");
		_ending2		= _doc.rootVisualElement.Q<Button>("EndingButton2");
		_ending3		= _doc.rootVisualElement.Q<Button>("EndingButton3");
		_ending4		= _doc.rootVisualElement.Q<Button>("EndingButton4");
		_ending5		= _doc.rootVisualElement.Q<Button>("EndingButton5");

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
		Debug.Log("Ending1");
	}
	private void Ending2Clicked()
	{
		Debug.Log("Ending2");
	}
	private void Ending3Clicked()
	{
		Debug.Log("Ending3");
	}
	private void Ending4Clicked()
	{
		Debug.Log("Ending4");
	}
	private void Ending5Clicked()
	{
		Debug.Log("Ending5");
	}
}
