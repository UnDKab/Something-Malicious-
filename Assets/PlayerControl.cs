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
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // �������� ����������� �������� �� ������ WASD, ���������� �� ����������� ������
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        // �������� ����������� �������� ������������ ����������� ������
        Vector3 worldDirection = Camera.main.transform.TransformDirection(direction);
        worldDirection.y = 0f; // �� �� �����, ����� �������� �������� ����� ��� ����

        // �����
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time - lastDashTime > dashCooldown)
        {
            if (!isDashing)
            {
                StartCoroutine(Dash());
            }
        }

        if (!isDashing)
        {
            // ��������� �������� � ������� ���������
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
