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
        // �����̴��� �ʱⰪ�� ���� ���� �������� ����
        volumeSlider.value = AudioManager.Instance.musicSource.volume;
        sfxSlider.value = AudioManager.Instance.sfxSource.volume;

        // �ؽ�Ʈ �ʱ�ȭ
        volumeText.text = (volumeSlider.value * 100).ToString("0");
        sfxText.text = (sfxSlider.value * 100).ToString("0");

        // �����̴� ���� ����� ������ ȣ��� �޼��� ����
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        sfxSlider.onValueChanged.AddListener(OnSfxChanged);

        debugPanel.SetActive(false); // ó������ �ɼ� �г��� ����ϴ�.
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
        // �� �����Ӹ��� �����̴� ���� Ȯ���Ͽ� ��Ʈ ���¸� ������Ʈ
        AudioManager.Instance.SetMusicVolume(volumeSlider.value);
        AudioManager.Instance.SetSFXVolume(sfxSlider.value);
    }

    void OnVolumeChanged(float volume)
    {
        // ���� ���� �ؽ�Ʈ�� ������Ʈ
        volumeText.text = (volume * 100).ToString("0");
    }

    void OnSfxChanged(float volume)
    {
        // ȿ���� ���� �ؽ�Ʈ�� ������Ʈ
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
