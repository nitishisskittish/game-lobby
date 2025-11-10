using UnityEngine;
using UnityEngine.InputSystem; // for Keyboard and Mouse input

public class CameraMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float lookSensitivity = 2f; // mouse look sensitivity

    private float pitch = 0f; // up/down
    private float yaw = 0f;   // left/right

    void Start()
    {
        // Lock the cursor to the center of the screen and hide it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Keyboard.current == null || Mouse.current == null) return;

        // --- Movement (WASD) ---
        float x = 0f;
        float z = 0f;
        if (Keyboard.current.aKey.isPressed) x = -1f;
        if (Keyboard.current.dKey.isPressed) x =  1f;
        if (Keyboard.current.wKey.isPressed) z =  1f;
        if (Keyboard.current.sKey.isPressed) z = -1f;

        Vector3 move = (transform.right * x + transform.forward * z).normalized;
        transform.position += move * moveSpeed * Time.deltaTime;

        // --- Mouse Look ---
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();
        yaw   += mouseDelta.x * lookSensitivity;
        pitch -= mouseDelta.y * lookSensitivity;
        pitch = Mathf.Clamp(pitch, -80f, 80f); // prevent flipping

        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }
}
