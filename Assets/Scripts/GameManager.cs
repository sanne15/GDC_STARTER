using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  private UIDocument _doc;
  private Button _startButton;
  private Button _quitButton;
  private Button _settingsButton;
  private Button _endingButton;
  private Button _loadButton;

  private FadeInOut fadeInOut;

	private void Awake()
	{
		_doc = GetComponent<UIDocument>();

    _startButton    = _doc.rootVisualElement.Q<Button>("StartButton");
    _quitButton     = _doc.rootVisualElement.Q<Button>("QuitButton");
    _settingsButton = _doc.rootVisualElement.Q<Button>("SettingsButton");
    _endingButton   = _doc.rootVisualElement.Q<Button>("EndingButton");
    _loadButton     = _doc.rootVisualElement.Q<Button>("LoadButton");

    _startButton.clicked    += StartButtonClicked;
    _quitButton.clicked     += QuitButtonClicked;
    _settingsButton.clicked += SettingsButtonClicked;
    _endingButton.clicked   += EndingButtonClicked;
    _loadButton.clicked     += LoadButtonClicked;

    fadeInOut = GameObject.Find("FadeOut Panel").GetComponent<FadeInOut>();
	}
  private void StartButtonClicked()
  {
    StartCoroutine(WaitAndExecute(1.5f));
  }
  IEnumerator WaitAndExecute(float waitTime)
  {
    fadeInOut.StartFadeInOut();
    yield return new WaitForSeconds(waitTime);
		SceneManager.LoadScene("OpeningScene");
	}
  private void QuitButtonClicked()
  {
    Application.Quit();
  }
  private void SettingsButtonClicked()
  {
		SceneManager.LoadScene("Settings");
	}
  private void EndingButtonClicked()
  {
		SceneManager.LoadScene("EndingCompilation");
	}
  private void LoadButtonClicked()
  {
    Debug.Log("저장 미구현...");
  }
}
