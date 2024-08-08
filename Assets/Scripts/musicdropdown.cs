using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class musicdropdown : MonoBehaviour
{
    public TMP_Dropdown musicDropdown;

    void Start()
    {
        PopulateDropdown();
        if (musicDropdown != null)
        {
            musicDropdown.onValueChanged.AddListener(OnDropdownValueChanged);
        }
    }

    void PopulateDropdown()
    {
        if (musicDropdown == null) return;

        musicDropdown.ClearOptions();


        // AudioManager Clips Loading
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
        foreach (var clip in AudioManager.Instance.musicClips)
        {
            options.Add(new TMP_Dropdown.OptionData(clip.name));
        }

        musicDropdown.AddOptions(options);
    }

    void OnDropdownValueChanged(int value)
    {
        string selectedMusicName = musicDropdown.options[value].text;
        AudioManager.Instance.PlayMusic(selectedMusicName);
    }
}
