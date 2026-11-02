using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    #region REFERENCES
    [Header("PUNTO DE AGARRE")]
    public Transform holdPoint;

    [Header("ELEMENTOS DE UI")]
    public GameObject interactionPanel;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI categoryText;
    #endregion

    #region SETTINGS
    [Header("CONFIGURACIÓN DE RAYCAST")]
    public float rayDistance = 4f;
    public LayerMask itemLayer;
    public KeyCode interactKey = KeyCode.E;
    #endregion

    #region PRIVATE VARIABLES
    private Transform cameraTransform;
    private WorldItem currentHeldItem;
    private WorldItem hoveredItem;
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
        if (currentHeldItem == null)
        {
            DetectItemWithRaycast();
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
        }
    }

    private void DetectItemWithRaycast()
    {
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hit, rayDistance, itemLayer))
        {
            WorldItem detectedItem = hit.transform.GetComponent<WorldItem>();

            if (detectedItem != null && detectedItem.itemData != null)
            {
                hoveredItem = detectedItem;
                ShowUI(detectedItem.itemData);
                return;
            }
        }

        ClearHover();
    }

    private void ShowUI(ItemScriptable data)
    {
        if (interactionPanel != null) interactionPanel.SetActive(true);
        if (nameText != null) nameText.text = data.itemName;
        if (priceText != null) priceText.text = $"${data.itemPrice:F0}";
        if (categoryText != null) categoryText.text = $"Cat: {data.category}";
    }

    private void ClearHover()
    {
        hoveredItem = null;
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