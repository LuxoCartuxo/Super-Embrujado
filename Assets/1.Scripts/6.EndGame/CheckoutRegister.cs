using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckoutRegister : MonoBehaviour
{
    #region REFERENCES
    [Header("UI DE LA BOLETA")]
    public GameObject receiptPanel;
    public TextMeshProUGUI receiptText;
    public BossEvaluation bossEvaluation;
    #endregion

    #region PUBLIC VARIABLES
    [HideInInspector]
    public bool purchaseCompleted = false;
    [HideInInspector]
    public float finalTotal = 0f;
    #endregion

    public void ProcessCheckout()
    {
        if (ShoppingCart.Instance == null || receiptPanel == null) return;

        List<ItemScriptable> items = ShoppingCart.Instance.itemsInCart;

        if (items.Count == 0)
        {
            print("[CAJA] El carrito está vacío. Agrega productos antes de comprar.");
            return;
        }

        float subtotal = 0f;
        float totalDiscount = 0f;

        int coffeeCount = 0;
        float coffeePrice = 0f;

        string receiptDetails = "<b>*** BOLETA DE COMPRA ***</b>\n\n";
        receiptDetails += "<b>PRODUCTOS COMPRADOS:</b>\n";

        for (int i = 0; i < items.Count; i++)
        {
            ItemScriptable item = items[i];
            subtotal += item.itemPrice;
            receiptDetails += $"- {item.itemName}: ${item.itemPrice:F0}\n";

            if (item.category == ItemCategory.Cafe)
            {
                coffeeCount++;
                coffeePrice = item.itemPrice;
            }
            else if (item.category == ItemCategory.Botella)
            {
                float discount = item.itemPrice * 0.5f;
                totalDiscount += discount;
            }
            else if (item.category == ItemCategory.Manzanas)
            {
                float discount = item.itemPrice * 0.25f;
                totalDiscount += discount;
            }
        }

        if (coffeeCount >= 2)
        {
            int freeCoffees = coffeeCount / 2;
            float coffeeDiscount = freeCoffees * coffeePrice;
            totalDiscount += coffeeDiscount;
        }

        finalTotal = Mathf.Max(0f, subtotal - totalDiscount);
        purchaseCompleted = true;

        receiptDetails += "\n----------------------------------------\n";
        receiptDetails += $"<b>SUBTOTAL:</b> ${subtotal:F0}\n";
        receiptDetails += $"<b>DESCUENTOS APLICADOS:</b> -${totalDiscount:F0}\n";
        receiptDetails += $"<size=120%><b>TOTAL FINAL:</b> ${finalTotal:F0}</size>\n\n";
        receiptDetails += "<i>(Presiona E para cerrar la boleta)</i>";

        if (receiptText != null) receiptText.text = receiptDetails;
        if (receiptPanel != null) receiptPanel.SetActive(true);
    }

    private void Update()
    {
        if (purchaseCompleted && receiptPanel != null && receiptPanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                CloseReceipt();
            }
        }
    }

    public void CloseReceipt()
    {
        if (receiptPanel != null) receiptPanel.SetActive(false);

        if (bossEvaluation != null)
        {
            bossEvaluation.TriggerEvaluationWithDelay(3f);
        }
    }
}