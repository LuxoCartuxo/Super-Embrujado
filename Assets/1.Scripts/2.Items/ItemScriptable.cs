using UnityEngine;

[CreateAssetMenu(fileName = "NewItemData", menuName = "Scriptables/ItemData")]
public class ItemScriptable : ScriptableObject
{
    #region VARIABLES
    [Header("DATOS DEL PRODUCTO")]
    public string itemName;
    public float itemPrice;
    public ItemCategory category;
    #endregion
}
public enum ItemCategory
{
    Botella,
    Donas,
    Cafe,
    Nova,
    Mermelada,
    ComidaEnlatada,
    Manzanas,
    Sandias,
    Papas
}