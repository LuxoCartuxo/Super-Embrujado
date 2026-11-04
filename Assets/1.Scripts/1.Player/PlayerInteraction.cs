using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    #region REFERENCES
    [Header("PUNTO DE AGARRE")]
    public Transform holdPoint;

    [Header("ELEMENTOS DE UI INTERACCIÓN DE PRODUCTO")]
    public GameObject interactionPanel;
    public TextMeshProUGUI productNameText;
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI categoryText;
    #endregion

    #region SETTINGS
    [Header("CONFIGURACIÓN DE RAYCAST")]
    public float rayDistance = 4f;
    public LayerMask interactableLayer;
    public KeyCode interactKey = KeyCode.E;
    #endregion

    #region PRIVATE VARIABLES
    private Transform cameraTransform;
    private WorldItem currentHeldItem;
    private WorldItem hoveredItem;
    private NPCInteractable hoveredNPC;
    #endregion

    private void Awake()
    {
        if (GetComponentInChildren<Camera>() != null)
        {
            cameraTransform = GetComponentInChildren<Camera>().transform;
        }
    }

    private void Update()
    {
        if (DialogueManager.Instance != null && DialogueManager.Instance.IsTalking())
        {
            if (Input.GetKeyDown(interactKey))
            {
                DialogueManager.Instance.AdvanceDialogue();
            }
            return;
        }

        if (currentHeldItem == null)
        {
            DetectWithRaycast();
        }

        if (Input.GetKeyDown(interactKey))
        {
            if (currentHeldItem != null)
            {
                DropHeldItem();
            }
            else if (hoveredItem != null)
            {
                PickupHoveredItem();
            }
            else if (hoveredNPC != null)
            {
                hoveredNPC.Interact();
                ClearHover();
            }
        }
    }

    private void DetectWithRaycast()
    {
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hit, rayDistance, interactableLayer))
        {
            WorldItem detectedItem = hit.transform.GetComponent<WorldItem>();
            if (detectedItem != null && detectedItem.itemData != null)
            {
                hoveredItem = detectedItem;
                hoveredNPC = null;
                ShowItemUI(detectedItem.itemData);
                return;
            }

            NPCInteractable detectedNPC = hit.transform.GetComponent<NPCInteractable>();
            if (detectedNPC != null)
            {
                hoveredNPC = detectedNPC;
                hoveredItem = null;
                if (interactionPanel != null) interactionPanel.SetActive(false);
                return;
            }
        }
        CheckoutRegister checkout = hit.transform.GetComponent<CheckoutRegister>();
        if (checkout != null)
        {
            if (Input.GetKeyDown(interactKey))
            {
                checkout.ProcessCheckout();
            }
            return;
        }

        ClearHover();
    }

    private void ShowItemUI(ItemScriptable data)
    {
        if (interactionPanel != null) interactionPanel.SetActive(true);
        if (productNameText != null) productNameText.text = data.itemName;
        if (priceText != null) priceText.text = $"${data.itemPrice:F0}";
        if (categoryText != null) categoryText.text = $"Cat: {data.category}";
    }

    private void ClearHover()
    {
        hoveredItem = null;
        hoveredNPC = null;
        if (interactionPanel != null) interactionPanel.SetActive(false);
    }

    private void PickupHoveredItem()
    {
        currentHeldItem = hoveredItem;
        currentHeldItem.PickupItem(holdPoint);
        ClearHover();
    }

    private void DropHeldItem()
    {
        if (currentHeldItem != null)
        {
            currentHeldItem.DropItem();
            currentHeldItem = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (cameraTransform != null)
        {
            Debug.DrawRay(cameraTransform.position, cameraTransform.forward * rayDistance, Color.magenta);
        }
    }
}