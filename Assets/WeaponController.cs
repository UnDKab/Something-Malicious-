using System.Linq;
using UnityEngine;

public enum Rarity
{
    Common,
    Uncommon,
    Rare,
    Unique,
    Legendary
}

public enum DamageType
{
    Normal,
    Holy,
    Cursed,
    Fire,
    Poison,
    Ice
}

[System.Serializable]
public struct WeaponStats
{
    public Rarity rarity;
    public DamageType damageType;
    public float damageMultiplier; // Множитель урона для данного уровня редкости
    public string weaponName; // Название оружия, к которому относится статистика
}

public class WeaponController : MonoBehaviour
{
    public GameObject[] weaponModels;
    public Transform spawnPoint;
    public WeaponStats[] weaponStats;

    // Добавляем свойство для получения общего урона
    public float TotalDamage
    {
        get
        {
            // Вычисляем общий урон, суммируя базовый урон каждого оружия с его множителем
            float totalDamage = 15f;
            foreach (var weaponStat in weaponStats)
            {
                // Находим соответствующую модель оружия
                GameObject selectedWeapon = weaponModels.FirstOrDefault(w => w.name == weaponStat.weaponName);
                if (selectedWeapon != null)
                {
                    WeaponModel weaponModel = selectedWeapon.GetComponent<WeaponModel>();
                    if (weaponModel != null)
                    {
                        totalDamage += weaponStat.damageMultiplier * weaponModel.baseDamage;
                    }
                }
            }
            return totalDamage;
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
        WeaponStats weaponStat = weaponStats[randomIndex];
        WeaponModel weaponModel = selectedWeapon.GetComponent<WeaponModel>(); // Получаем компонент WeaponModel

        float totalDamage = weaponModel.baseDamage * weaponStat.damageMultiplier; // Вычисляем общий урон

        GameObject weaponInstance = Instantiate(selectedWeapon, spawnPoint.position, Quaternion.identity);

        // Создаем attackPoint внутри weaponInstance
        GameObject attackPoint = new GameObject("AttackPoint");
        attackPoint.transform.parent = weaponInstance.transform;
        attackPoint.transform.localPosition = Vector3.zero;

        Debug.Log("Generated Weapon: Rarity - " + weaponStat.rarity + ", Damage Type: " + weaponStat.damageType + ", Total Damage: " + totalDamage);
    }
}
