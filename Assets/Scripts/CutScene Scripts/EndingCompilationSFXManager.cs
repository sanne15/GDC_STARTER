using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EndingCompilationSFXManager : MonoBehaviour
{
	public UIDocument _doc;
	public AudioSource audioSource;
	public AudioClip clickSFX;
	public AudioClip hoverSFX;

	private Button _backButton;
	private Button _ending1;
	private Button _ending2;
	private Button _ending3;
	private Button _ending4;
	private Button _ending5;

	void Awake()
	{
		_backButton = _doc.rootVisualElement.Q<Button>("BackButton");
		_ending1 = _doc.rootVisualElement.Q<Button>("EndingButton1");
		_ending2 = _doc.rootVisualElement.Q<Button>("EndingButton2");
		_ending3 = _doc.rootVisualElement.Q<Button>("EndingButton3");
		_ending4 = _doc.rootVisualElement.Q<Button>("EndingButton4");
		_ending5 = _doc.rootVisualElement.Q<Button>("EndingButton5");

		_ending1.clicked += PlayClickSFX;
		_ending2.clicked += PlayClickSFX;
		_ending3.clicked += PlayClickSFX;
		_ending4.clicked += PlayClickSFX;
		_ending5.clicked += PlayClickSFX;

		_backButton.RegisterCallback<MouseEnterEvent>(ev => PlayHoverSFX());
		_ending1.RegisterCallback<MouseEnterEvent>(ev => PlayHoverSFX());
		_ending2.RegisterCallback<MouseEnterEvent>(ev => PlayHoverSFX());
		_ending3.RegisterCallback<MouseEnterEvent>(ev => PlayHoverSFX());
		_ending4.RegisterCallback<MouseEnterEvent>(ev => PlayHoverSFX());
		_ending5.RegisterCallback<MouseEnterEvent>(ev => PlayHoverSFX());
	}

	void PlayClickSFX()
	{
		if (audioSource != null && clickSFX != null) audioSource.PlayOneShot(clickSFX);
	}

	void PlayHoverSFX()
	{
		if (audioSource != null && hoverSFX != null) audioSource.PlayOneShot(hoverSFX);
	}
}
