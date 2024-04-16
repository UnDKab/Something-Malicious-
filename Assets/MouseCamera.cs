using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCamera : MonoBehaviour
{
    public float sensitivity = .5f;
    private Vector2 rotation = Vector2.zero;
    public Transform playerBody;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        rotation.x += mouseX;
        rotation.y -= mouseY;
        rotation.y = Mathf.Clamp(rotation.y, -90f, 90f); // ќграничиваем угол вращени€ по вертикали

        transform.localRotation = Quaternion.Euler(rotation.y, rotation.x, 0);

        // ѕоворачиваем тело игрока по горизонтали
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
