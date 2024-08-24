using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue")]
public class Dialogue : ScriptableObject
{
    public string characterName;
    public string subcharName;
    public string altercharName;

    public List<SentenceData> sentences;

    void Awake()
    {
        subcharName = "¼­¿Õ¸ð";
        altercharName = "Áø¹«";
    }
    void OnEnable()
    {
        sentences = new List<SentenceData>();
    }
}
