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
            // Получить компонент PlayerAttack
            PlayerAttack playerAttack = player.GetComponent<PlayerAttack>();
            if (playerAttack != null)
            {
                // Установить attackPoint для PlayerAttack
                playerAttack.attackPoint = attackPoint.transform;
            }
            else
            {
                Debug.LogError("PlayerAttack component not found!");
            }
        }
        else
        {
            Debug.LogError("Player not found!");
        }

        Debug.Log(selectedWeapon);
    }

}
