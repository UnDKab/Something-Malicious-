using UnityEngine;

public class PickUpWeapon : MonoBehaviour
{
    public GameObject camera;
    public float distance = 15f;
    GameObject currentWeapon;
    bool canPickUp = false;
    PlayerAttack playerAttack;
    WeaponController weaponController; // Добавляем ссылку на WeaponController

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) PickUp();
        if (Input.GetKeyDown(KeyCode.Q)) Drop();
        if (playerAttack == null)
        {
            playerAttack = FindObjectOfType<PlayerAttack>();
            if (playerAttack == null)
            {
                Debug.LogError("PlayerAttack component not found!");
            }
        }
        if (weaponController == null)
        {
            weaponController = FindObjectOfType<WeaponController>();
            if (weaponController == null)
            {
                Debug.LogError("WeaponController component not found!");
            }
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

                // Получаем сохраненные координаты и повороты пивота оружия
                Vector3 pivotLocalPosition = currentWeapon.GetComponent<WeaponModel>().pivotLocalPosition;
                Quaternion pivotLocalRotation = currentWeapon.GetComponent<WeaponModel>().pivotLocalRotation;

                // Применяем сохраненные значения к позиции и ориентации оружия в руках игрока
                currentWeapon.transform.parent = transform;
                currentWeapon.transform.localPosition = pivotLocalPosition;
                currentWeapon.transform.localRotation = pivotLocalRotation;

                canPickUp = true;
                if (playerAttack != null)
                {
                    playerAttack.attackPoint = currentWeapon.transform.Find("AttackPoint");
                    if (playerAttack.attackPoint == null)
                    {
                        Debug.LogError("AttackPoint not found on the picked up weapon!");
                    }
                    playerAttack.weaponController = weaponController; // Передаем ссылку на WeaponController
                }
            }
        }
    }


    void Drop()
    {
        // Удаляем оружие из родительского объекта
        currentWeapon.transform.parent = null;

        // Возвращаем оружию его физические свойства
        currentWeapon.GetComponent<Rigidbody>().isKinematic = false;
        currentWeapon.GetComponent<Collider>().isTrigger = false;

        canPickUp = false;
        currentWeapon = null;

        if (playerAttack != null)
        {
            playerAttack.attackPoint = null;
            playerAttack.weaponController = null; // Сбрасываем ссылку на WeaponController
        }
    }
}