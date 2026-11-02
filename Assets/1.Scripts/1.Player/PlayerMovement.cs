using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region REFERENCES
    private Transform camTransform;
    #endregion

    #region VARIABLES
    [Header("CONFIGURACIÓN DE VISTA")]
    public float mouseSensitivity = 100f;
    private float verticalRotation;

    [Header("CONFIGURACIÓN DE MOVIMIENTO")]
    public float movementSpeed = 5f;
    #endregion

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (GetComponentInChildren<Camera>() != null)
        {
            camTransform = GetComponentInChildren<Camera>().transform;
        }
    }

    private void Update()
    {
        RotatePlayerAndCamera();
        MovePlayer();
    }

    private void RotatePlayerAndCamera()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * mouseSensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * mouseSensitivity;

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        transform.Rotate(0f, mouseX, 0f);

        if (camTransform != null)
        {
            camTransform.localEulerAngles = new Vector3(verticalRotation, 0f, 0f);
        }
    }

    private void MovePlayer()
    {
        float horizontalKeyboard = Input.GetAxisRaw("Horizontal") * Time.deltaTime * movementSpeed;
        float verticalKeyboard = Input.GetAxisRaw("Vertical") * Time.deltaTime * movementSpeed;

        transform.Translate(horizontalKeyboard, 0f, verticalKeyboard);
    }
}