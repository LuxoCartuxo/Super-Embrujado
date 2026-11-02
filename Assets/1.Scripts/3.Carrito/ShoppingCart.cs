using System.Collections.Generic;
using UnityEngine;

public class ShoppingCart : MonoBehaviour
{
    #region SINGLETON
    public static ShoppingCart Instance;
    #endregion

    #region VARIABLES
    [Header("INVENTARIO DEL CARRITO")]
    public List<ItemScriptable> itemsInCart = new List<ItemScriptable>();
    #endregion

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        WorldItem item = other.GetComponent<WorldItem>();

        if (item != null && item.itemData != null)
        {
            itemsInCart.Add(item.itemData);
            print($"[CARRITO] Producto agregado: {item.itemData.itemName} (${item.itemData.itemPrice})");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        WorldItem item = other.GetComponent<WorldItem>();

        if (item != null && item.itemData != null)
        {
            if (itemsInCart.Contains(item.itemData))
            {
                itemsInCart.Remove(item.itemData);
                print($"[CARRITO] Producto removido: {item.itemData.itemName}");
            }
        }
    }

    public float CalculateSubtotal()
    {
        float subtotal = 0f;

        for (int i = 0; i < itemsInCart.Count; i++)
        {
            subtotal += itemsInCart[i].itemPrice;
        }

        return subtotal;
    }
}