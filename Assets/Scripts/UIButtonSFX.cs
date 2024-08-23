using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIButtonSFX : MonoBehaviour
{
  public UIDocument   uiDocument; // UI Document를 연결합니다.
  public AudioSource  audioSource; // Audio Source를 연결합니다.
  public AudioClip    clickSFX; // 버튼 클릭 SFX를 연결합니다.
	public AudioClip    hoverSFX;

	private Button _startButton;
  private Button _quitButton;
  private Button _settingsButton;
  private Button _endingButton;
  private Button _loadButton;

  void Start()
  {
    // VisualElement에 접근하기 위해 UI Document에서 rootVisualElement를 가져옵니다.
    var root = uiDocument.rootVisualElement;

    // 버튼을 찾아서 클릭 이벤트를 연결합니다.
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
    // 버튼이 클릭되었을 때 SFX 재생
    if (audioSource != null && clickSFX != null)
    {
        audioSource.PlayOneShot(clickSFX);
    }
  }
  void QuitButtonClicked()
  {
    // 버튼이 클릭되었을 때 SFX 재생
    if (audioSource != null && clickSFX != null)
    {
        audioSource.PlayOneShot(clickSFX);
    }
  }
  void SettingsButtonClicked()
  {
    // 버튼이 클릭되었을 때 SFX 재생
    if (audioSource != null && clickSFX != null)
    {
        audioSource.PlayOneShot(clickSFX);
    }
  }
  void EndingButtonClicked()
  {
    // 버튼이 클릭되었을 때 SFX 재생
    if (audioSource != null && clickSFX != null)
    {
        audioSource.PlayOneShot(clickSFX);
    }
  }
  void LoadButtonClicked()
  {
    // 버튼이 클릭되었을 때 SFX 재생
    if (audioSource != null && clickSFX != null)
    {
        audioSource.PlayOneShot(clickSFX);
    }
  }

	void PlayHoverSFX()
	{
		// 마우스가 버튼 위에 올라갔을 때 SFX 재생
		if (audioSource != null && hoverSFX != null)
		{
			audioSource.PlayOneShot(hoverSFX);
		}
	}
}
