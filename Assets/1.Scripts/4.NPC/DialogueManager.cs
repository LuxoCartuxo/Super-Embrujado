using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    #region SINGLETON
    public static DialogueManager Instance;
    #endregion

    #region REFERENCES
    [Header("PANEL Y TEXTOS DE UI")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI phraseText;
    #endregion

    #region PRIVATE VARIABLES
    private ConversationScriptable currentConversation;
    private int currentDialogueIndex;
    private bool isTalking;
    #endregion

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        ClearUI();
    }

    public void StartConversation(ConversationScriptable conversation)
    {
        if (conversation == null || conversation.dialogueArray.Length == 0) return;

        currentConversation = conversation;
        currentDialogueIndex = 0;
        isTalking = true;

        if (dialoguePanel != null) dialoguePanel.SetActive(true);
        RefreshDialogueUI();
    }

    public void AdvanceDialogue()
    {
        if (!isTalking || currentConversation == null) return;

        if (currentDialogueIndex < currentConversation.dialogueArray.Length - 1)
        {
            currentDialogueIndex++;
            RefreshDialogueUI();
        }
        else
        {
            EndConversation();
        }
    }

    public void EndConversation()
    {
        ClearUI();
        isTalking = false;
        currentConversation = null;
        if (dialoguePanel != null) dialoguePanel.SetActive(false);
    }

    private void ClearUI()
    {
        if (nameText != null) nameText.text = "";
        if (phraseText != null) phraseText.text = "";
        currentDialogueIndex = 0;
    }

    private void RefreshDialogueUI()
    {
        if (currentConversation != null && currentDialogueIndex < currentConversation.dialogueArray.Length)
        {
            nameText.text = currentConversation.dialogueArray[currentDialogueIndex].characterName;
            phraseText.text = currentConversation.dialogueArray[currentDialogueIndex].phrase;
        }
    }

    public bool IsTalking()
    {
        return isTalking;
    }
}