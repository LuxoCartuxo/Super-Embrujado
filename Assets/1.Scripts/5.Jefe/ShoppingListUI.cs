using UnityEngine;
using TMPro;

public class ShoppingListUI : MonoBehaviour
{
    #region REFERENCES
    [Header("ELEMENTOS DE UI")]
    public GameObject listPanel;
    public TextMeshProUGUI listContentText;
    #endregion

    #region SETTINGS
    [Header("CONTROLES")]
    public KeyCode toggleKey = KeyCode.Tab;
    private bool isPanelOpen = false;
    #endregion

    private void Start()
    {
        if (listPanel != null) listPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            ToggleList();
        }
    }

    private void ToggleList()
    {
        isPanelOpen = !isPanelOpen;

        if (listPanel != null)
        {
            listPanel.SetActive(isPanelOpen);
        }

        if (isPanelOpen)
        {
            UpdateListDisplay();
        }
    }

    private void UpdateListDisplay()
    {
        if (listContentText == null || BossManager.Instance == null) return;

        var currentList = BossManager.Instance.requestedList;

        if (currentList.Count == 0)
        {
            listContentText.text = "Lista vacía. Habla con tu jefe.";
            return;
        }

        string formattedText = "<b>LISTA DE COMPRAS DEL JEFE:</b>\n\n";

        for (int i = 0; i < currentList.Count; i++)
        {
            formattedText += $"- {currentList[i].itemName}\n";
        }

        listContentText.text = formattedText;
    }
}
