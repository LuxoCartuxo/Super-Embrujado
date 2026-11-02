using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class WorldItem : MonoBehaviour
{
    #region REFERENCES
    [Header("CONFIGURACIÓN DE DATOS")]
    public ItemScriptable itemData;
    #endregion

    #region PROPERTIES
    public Rigidbody Rb { get; private set; }
    #endregion

    private void Awake()
    {
        Rb = GetComponent<Rigidbody>();
    }

    public void PickupItem(Transform holdPoint)
    {
        Rb.useGravity = false;
        Rb.isKinematic = true;
        transform.position = holdPoint.position;
        transform.parent = holdPoint;
    }

    public void DropItem()
    {
        transform.parent = null;
        Rb.useGravity = true;
        Rb.isKinematic = false;
    }
}