using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Slider volumeSlider;
    public TMP_Text volumeText;
    public Slider sfxSlider;
    public TMP_Text sfxText;

    public GameObject debugPanel;
    public Button hideDebugButton;
    public Button settingButton;

    public GameObject optionPanel;
    public Button hideOptionButton;
    public Button settingOptionButton;

    void Start()
    {
        // 슬라이더의 초기값을 현재 음악 볼륨으로 설정
        volumeSlider.value = AudioManager.Instance.musicSource.volume;
        sfxSlider.value = AudioManager.Instance.sfxSource.volume;

        // 텍스트 초기화
        volumeText.text = (volumeSlider.value * 100).ToString("0");
        sfxText.text = (sfxSlider.value * 100).ToString("0");

        // 슬라이더 값이 변경될 때마다 호출될 메서드 설정
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        sfxSlider.onValueChanged.AddListener(OnSfxChanged);

        debugPanel.SetActive(false); // 처음에는 옵션 패널을 숨깁니다.
        optionPanel.SetActive(false);

        hideDebugButton.onClick.AddListener(HideDebugPanel);
        settingButton.onClick.AddListener(ShowDebugPanel);
        hideOptionButton.onClick.AddListener(HideOptionPanel);
        settingOptionButton.onClick.AddListener(ShowOptionPanel);

        AddEventTrigger(optionPanel);
        AddEventTrigger(debugPanel);
    }

    void Update()
    {
        // 매 프레임마다 슬라이더 값을 확인하여 뮤트 상태를 업데이트
        AudioManager.Instance.SetMusicVolume(volumeSlider.value);
        AudioManager.Instance.SetSFXVolume(sfxSlider.value);
    }

    void OnVolumeChanged(float volume)
    {
        // 볼륨 값을 텍스트에 업데이트
        volumeText.text = (volume * 100).ToString("0");
    }

    void OnSfxChanged(float volume)
    {
        // 효과음 값을 텍스트에 업데이트
        sfxText.text = (volume * 100).ToString("0");
    }

    void AddEventTrigger(GameObject panel)
    {
        EventTrigger trigger = panel.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = panel.AddComponent<EventTrigger>();
        }

        EventTrigger.Entry entry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerDown
        };
        entry.callback.AddListener((data) => { OnPanelClicked(panel); });
        trigger.triggers.Add(entry);
    }

    public void OnPanelClicked(GameObject panel)
    {
        panel.transform.SetAsLastSibling();
    }

    void HideDebugPanel()
    {
        debugPanel.SetActive(false);
    }

    void ShowDebugPanel()
    {
        debugPanel.SetActive(true);
    }

    void HideOptionPanel()
    {
        optionPanel.SetActive(false);
    }

    void ShowOptionPanel()
    {
        optionPanel.SetActive(true);
    }
}
