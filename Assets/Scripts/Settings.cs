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
        // 슬라이더 초기값 설정
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1.0f);
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1.0f);
    }

    public void OnSFXVolumeChanged()
    {
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
        // 실제 SFX 볼륨 조절 코드 추가
    }

    public void OnVolumeChanged()
    {
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        // 실제 볼륨 조절 코드 추가
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
