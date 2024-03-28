using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 50f;
    public float dashDistance = 5f;
    public float dashCooldown = 4f;
    private bool isDashing = false;
    private float lastDashTime = -999f;

    // Update is called once per frame
    void Update()
    {
        // ѕолучаем направление движени€ от клавиш WASD, независимо от направлени€ камеры
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // ѕолучаем направление движени€ относительно направлени€ камеры
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;
        cameraForward.y = 0f;
        cameraRight.y = 0f;
        Vector3 worldDirection = (cameraForward * vertical + cameraRight * horizontal).normalized;

        // –ывок
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time - lastDashTime > dashCooldown)
        {
            if (!isDashing)
            {
                StartCoroutine(Dash());
            }
        }

        if (!isDashing)
        {
            // ѕримен€ем скорость и двигаем персонажа
            transform.Translate(worldDirection * speed * Time.deltaTime, Space.World);
        }
    }

    IEnumerator Dash()
    {
        isDashing = true;
        lastDashTime = Time.time;
        Vector3 dashDestination = transform.position + Camera.main.transform.forward * dashDistance;

        while (Vector3.Distance(transform.position, dashDestination) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, dashDestination, speed * Time.deltaTime * 2f);
            yield return null;
        }

        isDashing = false;
    }
}
