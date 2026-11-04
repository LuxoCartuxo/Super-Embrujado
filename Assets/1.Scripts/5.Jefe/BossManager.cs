using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    #region SINGLETON
    public static BossManager Instance;
    #endregion

    #region REFERENCES
    [Header("BASE DE DATOS DE PRODUCTOS")]
    public List<ItemScriptable> availableProducts = new List<ItemScriptable>();

    [Header("DIÁLOGO DEL JEFE")]
    [Tooltip("Arrastra aquí el ScriptableObject de la conversación inicial del Jefe")]
    public ConversationScriptable bossConversation;
    #endregion

    #region SETTINGS
    [Header("CONFIGURACIÓN DE ALEATORIEDAD")]
    public int minItemsCount = 3;
    public int maxItemsCount = 7;
    #endregion

    #region PUBLIC VARIABLES
    [HideInInspector]
    public List<ItemScriptable> requestedList = new List<ItemScriptable>();
    #endregion

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        GenerateRandomShoppingList();

        if (bossConversation != null && DialogueManager.Instance != null)
        {
            DialogueManager.Instance.StartConversation(bossConversation);
        }
    }

    public void GenerateRandomShoppingList()
    {
        requestedList.Clear();

        if (availableProducts.Count == 0) return;

        int targetAmount = Random.Range(minItemsCount, maxItemsCount + 1);

        for (int i = 0; i < targetAmount; i++)
        {
            int randomIndex = Random.Range(0, availableProducts.Count);
            requestedList.Add(availableProducts[randomIndex]);
        }
    }
}