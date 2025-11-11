using UnityEngine;
using UnityEngine.InputSystem; // for Keyboard and Mouse input

public class CameraMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Update()
    {
        if (Keyboard.current == null || Mouse.current == null) return;

        //WASD
        float x = 0f;
        float z = 0f;
        if (Keyboard.current.aKey.isPressed) x = -1f;
        if (Keyboard.current.dKey.isPressed) x =  1f;
        if (Keyboard.current.wKey.isPressed) z =  1f;
        if (Keyboard.current.sKey.isPressed) z = -1f;

        Vector3 move = (transform.right * x + transform.forward * z).normalized;
        transform.position += move * moveSpeed * Time.deltaTime;
    }
}
