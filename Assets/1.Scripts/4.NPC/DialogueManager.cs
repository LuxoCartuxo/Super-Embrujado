using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    #region REFERENCES
    public ConversationScriptable currentConversation;
    public TextMeshProUGUI nameText, phraseText;
    #endregion

    #region PRIVATE VARIABLES
    private int currentDialogueIndex;
    private bool isTalking;
    #endregion

    private void Start()
    {
        ClearUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Talk();
        }
    }
    private void ClearUI()
    {
        nameText.text = null;
        phraseText.text = null;
        currentDialogueIndex = 0;
    }
    private void Talk()
    {
        if (isTalking)
        {
            if (currentDialogueIndex < currentConversation.dialogueArray.Length - 1)
            {
                currentDialogueIndex++;
                RefreshDialogueUI();
            }
            else
            {
                ClearUI();
                isTalking = false;
            }
        }
        else
        {
            RefreshDialogueUI();
            isTalking = true;
        }
    }
    private void RefreshDialogueUI()
    {
        nameText.text = currentConversation.dialogueArray[currentDialogueIndex].characterName;
        phraseText.text = currentConversation.dialogueArray[currentDialogueIndex].phrase;
    }
}
