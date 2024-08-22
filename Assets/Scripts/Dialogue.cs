using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue")]
public class Dialogue : ScriptableObject
{
    public string characterName;
    public List<SentenceData> sentences;

    void OnEnable()
    {
        sentences = new List<SentenceData>();
    }
}
