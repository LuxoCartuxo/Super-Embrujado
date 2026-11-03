using UnityEngine;
using System;
using TMPro;

[CreateAssetMenu(fileName = "New Conversation", menuName = "Scriptables/Conversation")]

public class ConversationScriptable : ScriptableObject
{
    public DIALOGUE[] dialogueArray;
}

[Serializable]
public struct DIALOGUE
{
    public string characterName;
    public string phrase;
}
