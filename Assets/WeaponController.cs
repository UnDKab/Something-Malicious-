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
    }

}
