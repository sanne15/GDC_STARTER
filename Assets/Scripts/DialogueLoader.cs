using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueDatabase
{
    public List<StoryNPCData> storyNPCs;
}

[System.Serializable]
public class StoryNPCData
{
    public string name;
    public DialogueData dialogue;
}

[System.Serializable]
public class DialogueData
{
    public string characterName;
    public string[] sentences;
}

public class DialogueLoader : MonoBehaviour
{
    private List<string> storyNPCNames;
    private Dictionary<string, Dialogue> storyNPCDialogues;

    public void LoadDialogueData(int day)
    {
        // day에 맞는 JSON 파일을 Resources에서 로드
        string filePath = $"Story/day_{day}";
        TextAsset dialogueJSON = Resources.Load<TextAsset>(filePath);

        if (dialogueJSON == null)
        {
            Debug.LogError($"Dialogue file for day {day} not found at {filePath}");
            return;
        }

        storyNPCDialogues = new Dictionary<string, Dialogue>();
        storyNPCNames = new List<string>();

        DialogueDatabase dialogueDatabase = JsonUtility.FromJson<DialogueDatabase>(dialogueJSON.text);

        if (dialogueDatabase != null)
        {
            foreach (var npcData in dialogueDatabase.storyNPCs)
            {
                Dialogue newDialogue = ScriptableObject.CreateInstance<Dialogue>();
                newDialogue.characterName = npcData.dialogue.characterName;
                newDialogue.sentences = npcData.dialogue.sentences;

                storyNPCDialogues[npcData.name] = newDialogue;
                storyNPCNames.Add(npcData.name);
            }
            // Debug.Log($"Loaded {storyNPCNames.Count} Story NPCs for day {day}");
        }
        else
        {
            // Debug.LogError("Failed to parse DialogueDatabase from JSON.");
        }
    }

    public Dialogue GetStoryNPCDialogue(string npcName)
    {

        if (storyNPCDialogues != null && storyNPCDialogues.ContainsKey(npcName))
        {
            return storyNPCDialogues[npcName];
        }

        Debug.LogWarning($"NPC: {npcName} not found in storyNPCDialogues.");
        return null;
    }

    public List<string> GetStoryNPCNames()
    {
        return storyNPCNames;
    }
}
