using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject[] weaponModels; // Массив моделей оружия
    public Transform spawnPoint; // Точка на земле, где будет появляться оружие

    void Start()
    {
        // Выбираем случайную модель оружия
        int randomIndex = Random.Range(0, weaponModels.Length);
        GameObject selectedWeapon = weaponModels[randomIndex];

        // Создаем экземпляр выбранной модели и помещаем его на землю
        GameObject weaponInstance = Instantiate(selectedWeapon, spawnPoint.position, Quaternion.identity);
        Debug.Log(selectedWeapon);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) GenerateWeapon();
    }

    void GenerateWeapon () 
    {
        int randomIndex = Random.Range(0, weaponModels.Length);
        GameObject selectedWeapon = weaponModels[randomIndex];

        GameObject weaponInstance = Instantiate(selectedWeapon, spawnPoint.position, Quaternion.identity);
        Debug.Log(selectedWeapon);
    }
}
