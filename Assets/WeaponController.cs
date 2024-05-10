using System.Linq;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject[] weaponModels;
    public Transform spawnPoint;
    public WeaponData[] weaponData;

    public float TotalDamage
    {
        get
        {
            float totalDamage = 0f;
            foreach (var data in weaponData)
            {
                totalDamage += CalculateDamage(data);
            }
            return totalDamage;
        }
    }

    float CalculateDamage(WeaponData data)
    {
        float rarityMultiplier = GetRarityMultiplier(data.rarity);
        return data.baseDamage * rarityMultiplier;
    }

    float GetRarityMultiplier(Rarity rarity)
    {
        switch (rarity)
        {
            case Rarity.Common:
                return 1.0f;
            case Rarity.Uncommon:
                return 1.1f;
            case Rarity.Rare:
                return 1.2f;
            case Rarity.Unique:
                return 1.3f;
            case Rarity.Legendary:
                return 1.5f;
            default:
                return 1.0f;
        }
    }

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
        WeaponData data = weaponData[randomIndex];

        float totalDamage = CalculateDamage(data);

        GameObject weaponInstance = Instantiate(selectedWeapon, spawnPoint.position, Quaternion.identity);

        GameObject attackPoint = new GameObject("AttackPoint");
        attackPoint.transform.parent = weaponInstance.transform;
        attackPoint.transform.localPosition = Vector3.zero;

        Debug.Log("Generated Weapon: " + selectedWeapon.name + ", Rarity - " + data.rarity + ", Damage Type: " + data.damageType + ", Total Damage: " + totalDamage);
    }
}
