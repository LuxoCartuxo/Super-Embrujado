using UnityEngine;

public class NPCInteractable : MonoBehaviour
{
    #region REFERENCES
    [Header("CONVERSACIÓN DEL NPC")]
    [Tooltip("Arrastra aquí el ScriptableObject de la conversación de este NPC")]
    public ConversationScriptable npcConversation;
    #endregion

    public void Interact()
    {
        if (npcConversation != null && DialogueManager.Instance != null)
        {
            DialogueManager.Instance.StartConversation(npcConversation);
        }
    }
}