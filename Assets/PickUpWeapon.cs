using UnityEngine;

public class PickUpWeapon : MonoBehaviour
{
    public GameObject camera;
    public float distance = 15f;
    GameObject currentWeapon;
    bool canPickUp = false;
    PlayerAttack playerAttack; // ������ �� ��������� PlayerAttack

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) PickUp();
        if (Input.GetKeyDown(KeyCode.Q)) Drop();
        playerAttack = FindObjectOfType<PlayerAttack>();
        if (playerAttack == null)
        {
            Debug.LogError("PlayerAttack component not found!");
        }
    }

    void PickUp()
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, distance))
        {
            if (hit.transform.tag == "Weapon")
            {
                if (canPickUp) Drop();

                currentWeapon = hit.transform.gameObject;
                currentWeapon.GetComponent<Collider>().isTrigger = true;
                currentWeapon.GetComponent<Rigidbody>().isKinematic = true;
                currentWeapon.transform.parent = transform;
                currentWeapon.transform.localPosition = Vector3.zero;
                currentWeapon.transform.localEulerAngles = new Vector3(0f, -90f, 0f);
                canPickUp = true;

                if (playerAttack != null)
                {
                    playerAttack.attackPoint = currentWeapon.transform.Find("AttackPoint");
                    if (playerAttack.attackPoint == null)
                    {
                        Debug.LogError("AttackPoint not found on the picked up weapon!");
                    }
                }
            }
        }
    }

    void Drop()
    {
        currentWeapon.transform.parent = null;
        currentWeapon.GetComponent<Rigidbody>().isKinematic = false;
        currentWeapon.GetComponent<Collider>().isTrigger = false;
        canPickUp = false;
        currentWeapon = null;

        if (playerAttack != null)
        {
            playerAttack.attackPoint = null;
        }
    }
}
