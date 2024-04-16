using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject[] weaponModels;
    public Transform spawnPoint;

    void Start()
    {
        GenerateWeapon();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            GenerateWeapon();
        }
    }

    void GenerateWeapon()
    {
        int randomIndex = Random.Range(0, weaponModels.Length);
        GameObject selectedWeapon = weaponModels[randomIndex];

        GameObject weaponInstance = Instantiate(selectedWeapon, spawnPoint.position, Quaternion.identity);

        // Создаем attackPoint внутри weaponInstance
        GameObject attackPoint = new GameObject("AttackPoint");
        attackPoint.transform.parent = weaponInstance.transform;
        attackPoint.transform.localPosition = Vector3.zero;

        // Найти объект с тегом "Player"
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            // Делаем оружие дочерним объектом тела игрока
            weaponInstance.transform.parent = player.transform;

            // Перемещаем оружие в нужную позицию относительно тела игрока
            weaponInstance.transform.localPosition = Vector3.zero; // Переместить оружие в центр тела игрока или на другую позицию, если нужно
            weaponInstance.transform.localRotation = Quaternion.identity; // Сбросить поворот оружия
        }
        else
        {
            Debug.LogError("Player not found!");
        }

        Debug.Log(selectedWeapon);
    }

}
