using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Slider sfxSlider;
    public Slider volumeSlider;

    private void Start()
    {
        // �����̴� �ʱⰪ ����
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1.0f);
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1.0f);
    }

    public void OnSFXVolumeChanged()
    {
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
        // ���� SFX ���� ���� �ڵ� �߰�
    }

    public void OnVolumeChanged()
    {
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        // ���� ���� ���� �ڵ� �߰�
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
