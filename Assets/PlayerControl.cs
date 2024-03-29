using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 50f;
    public float dashDistance = 5f;
    public float dashCooldown = 4f;
    public float runSpeedMultiplier = 2f; // Множитель скорости бега
    private bool isDashing = false;
    private bool isRunning = false;
    private float lastDashTime = -999f;

    // Update is called once per frame
    void Update()
    {
        // Получаем направление движения от клавиш WASD, независимо от направления камеры
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Получаем направление движения относительно направления камеры
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;
        cameraForward.y = 0f;
        cameraRight.y = 0f;
        Vector3 worldDirection = (cameraForward * vertical + cameraRight * horizontal).normalized;

        // Рывок на левом CTRL
        if (Input.GetKeyDown(KeyCode.LeftControl) && Time.time - lastDashTime > dashCooldown)
        {
            if (!isDashing)
            {
                StartCoroutine(Dash());
            }
            else if (isRunning) // Если рывок активен и игрок бежит, отменить бег
            {
                isRunning = false;
                speed /= runSpeedMultiplier;
            }
        }
        else if (Input.GetKey(KeyCode.LeftShift) && !isDashing) // Бег при удержании левого Shift
        {
            if (!isRunning)
            {
                isRunning = true;
                speed *= runSpeedMultiplier;
            }
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) && isRunning) // Когда кнопка левого Shift отпущена, прекратить бег
        {
            isRunning = false;
            speed /= runSpeedMultiplier;
        }

        if (!isDashing)
        {
            // Применяем скорость и двигаем персонажа
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