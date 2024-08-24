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
        // ���� ����Ǿ �ı����� �ʵ��� ����
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
        // ���� ������ ����Ǹ� MusicManager�� �ı�
        if (scene.name == "Opening")
        {
            SceneManager.sceneLoaded -= OnSceneLoaded; // �̺�Ʈ ���� ����
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
            audioSource.Play(); // AudioSource�� �̸� ������ Ŭ���� �ִٸ� �ڵ����� ���
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

    // ���� ��� �ð��� ��ȯ
    public float GetMusicTime()
    {
        return audioSource.time;
    }

    // Ư�� �������� ���
    public void PlayMusicFromTime(float time)
    {
        audioSource.time = time;
        audioSource.Play();
    }
}

