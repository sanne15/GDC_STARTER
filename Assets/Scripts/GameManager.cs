using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public UIDocument _doc;
    private Button _startButton;
    private Button _quitButton;
    private Button _settingsButton;
    private Button _endingButton;
    private Button _loadButton;

    private FadeInScript _fadeInScript;

    private void Awake()
    {
        var root = _doc.rootVisualElement;
    
        if (root == null)
        {
            Debug.LogError("rootVisualElement is null!");
            return;
        }

        _startButton = root.Q<Button>("StartButton");
        _quitButton = root.Q<Button>("QuitButton");
        _settingsButton = root.Q<Button>("SettingsButton");
        _endingButton = root.Q<Button>("EndingButton");
        _loadButton = root.Q<Button>("LoadButton");

            if (_startButton == null)
        {
            Debug.LogError("StartButton not found in the UI document.");
        }

        _startButton.clicked    += StartButtonClicked;
        _quitButton.clicked     += QuitButtonClicked;
        _settingsButton.clicked += SettingsButtonClicked;
        _endingButton.clicked   += EndingButtonClicked;
        _loadButton.clicked     += LoadButtonClicked;

        _fadeInScript = GameObject.Find("GoToOpeningScene Panel").GetComponent<FadeInScript>();

    }

    private void StartButtonClicked()
    {
        _fadeInScript.StartFadeIn();
        Invoke("GoToOpeningScene", _fadeInScript.fadeTime + 1.0f);
    }

    private void GoToOpeningScene()
    {
        StartCoroutine(Loading(1.1f, "NameScene"));
    }

    private void QuitButtonClicked()
    {
        Application.Quit();
    }

    private void SettingsButtonClicked()
    {
        StartCoroutine(Loading(1.1f, "Settings"));
    }

    private void EndingButtonClicked()
    {
	    StartCoroutine(Loading(1.1f, "EndingCompilation"));
    }

    private void LoadButtonClicked()
    {
        Debug.Log("저장 미구현...");
    }

    IEnumerator Loading(float delay, string message)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(message);
    }
}
