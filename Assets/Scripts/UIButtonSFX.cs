using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIButtonSFX : MonoBehaviour
{
  public UIDocument   uiDocument; // UI Document�� �����մϴ�.
  public AudioSource  audioSource; // Audio Source�� �����մϴ�.
  public AudioClip    clickSFX; // ��ư Ŭ�� SFX�� �����մϴ�.
	public AudioClip    hoverSFX;

	private Button _startButton;
  private Button _quitButton;
  private Button _settingsButton;
  private Button _endingButton;
  private Button _loadButton;

  void Start()
  {
    // VisualElement�� �����ϱ� ���� UI Document���� rootVisualElement�� �����ɴϴ�.
    var root = uiDocument.rootVisualElement;

    // ��ư�� ã�Ƽ� Ŭ�� �̺�Ʈ�� �����մϴ�.
    _startButton    = root.Q<Button>("StartButton");
    _quitButton     = root.Q<Button>("QuitButton");
    _settingsButton = root.Q<Button>("SettingsButton");
    _endingButton   = root.Q<Button>("EndingButton");
    _loadButton     = root.Q<Button>("LoadButton");

    _startButton.clicked    += StartButtonClicked;
    _quitButton.clicked     += QuitButtonClicked;
    _settingsButton.clicked += SettingsButtonClicked;
    _endingButton.clicked   += EndingButtonClicked;
    _loadButton.clicked     += LoadButtonClicked;

		_startButton.RegisterCallback<MouseEnterEvent>(ev => PlayHoverSFX());
		_quitButton.RegisterCallback<MouseEnterEvent>(ev => PlayHoverSFX());
		_settingsButton.RegisterCallback<MouseEnterEvent>(ev => PlayHoverSFX());
		_endingButton.RegisterCallback<MouseEnterEvent>(ev => PlayHoverSFX());
		_loadButton.RegisterCallback<MouseEnterEvent>(ev => PlayHoverSFX());
	}

  void StartButtonClicked()
  {
    // ��ư�� Ŭ���Ǿ��� �� SFX ���
    if (audioSource != null && clickSFX != null)
    {
        audioSource.PlayOneShot(clickSFX);
    }
  }
  void QuitButtonClicked()
  {
    // ��ư�� Ŭ���Ǿ��� �� SFX ���
    if (audioSource != null && clickSFX != null)
    {
        audioSource.PlayOneShot(clickSFX);
    }
  }
  void SettingsButtonClicked()
  {
    // ��ư�� Ŭ���Ǿ��� �� SFX ���
    if (audioSource != null && clickSFX != null)
    {
        audioSource.PlayOneShot(clickSFX);
    }
  }
  void EndingButtonClicked()
  {
    // ��ư�� Ŭ���Ǿ��� �� SFX ���
    if (audioSource != null && clickSFX != null)
    {
        audioSource.PlayOneShot(clickSFX);
    }
  }
  void LoadButtonClicked()
  {
    // ��ư�� Ŭ���Ǿ��� �� SFX ���
    if (audioSource != null && clickSFX != null)
    {
        audioSource.PlayOneShot(clickSFX);
    }
  }

	void PlayHoverSFX()
	{
		// ���콺�� ��ư ���� �ö��� �� SFX ���
		if (audioSource != null && hoverSFX != null)
		{
			audioSource.PlayOneShot(hoverSFX);
		}
	}
}
