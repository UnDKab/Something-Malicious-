using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject arrowPrefab; // ������ �� ������ ������
    private float timer; // ������ ��� ������������ �����������
    public float reloadTime = 1.0f; // ����� ����������� � ��������
    private Animator bowAnimator; // ������ �� ��������� Animator

    void Start()
    {
        // �������� ������ ����
        Cursor.visible = false;

        // �������� ��������� Animator
        bowAnimator = GetComponent<Animator>();
        if (bowAnimator == null)
        {
            Debug.LogError("Animator component not found!");
        }
    }

    void Update()
    {
        // ��������� ������ �� ���������� �������, ��������� � ���������� �����
        timer -= Time.deltaTime;

        // ���������, ������ �� ������ ���� � ����� �� ������ �����������
        if (Input.GetMouseButton(0) && timer <= 0)
        {
            // ������������� �������� �����
            if (bowAnimator != null)
            {
                bowAnimator.SetTrigger("Attack");
            }

            // ������� ��������� ������ �� ������� �� ������� � � �������� �������� �������
            GameObject arrowInstance = Instantiate(arrowPrefab, transform.position, transform.rotation);

            // ��������� ���� � Rigidbody ������, ����� ��������� �� ������
            Rigidbody arrowRigidbody = arrowInstance.GetComponent<Rigidbody>();
            if (arrowRigidbody != null)
            {
                arrowRigidbody.AddForce(transform.forward * 1000f);
            }

            // ����� ������� �����������
            timer = reloadTime;
        }
    }
}
