using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    public AudioSource audioSource;

    void Awake()
    {
        // 씬이 변경되어도 파괴되지 않도록 설정
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 게임 씬으로 변경되면 MusicManager를 파괴
        if (scene.name == "Opening")
        {
            SceneManager.sceneLoaded -= OnSceneLoaded; // 이벤트 구독 해제
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        if (audioSource.clip != null && !audioSource.isPlaying)
        {
            audioSource.Play(); // AudioSource에 미리 지정된 클립이 있다면 자동으로 재생
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        if (audioSource.clip != clip)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }

    // 현재 재생 시간을 반환
    public float GetMusicTime()
    {
        return audioSource.time;
    }

    // 특정 지점부터 재생
    public void PlayMusicFromTime(float time)
    {
        audioSource.time = time;
        audioSource.Play();
    }
}

