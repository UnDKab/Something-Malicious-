using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerStats : MonoBehaviour
{
    // Статы игрока
    public int healthPoints = 100;
    public int manaPoints = 100;
    public float movementSpeed = 5.0f;
    public bool hasPassiveSkill = false;

    // Параметры масок
    public enum MaskType { Clean, Thief, Jester, Chaos }
    public MaskType currentMask = MaskType.Clean;

    // Событие для обновления интерфейса или других систем при изменении статов
    public event Action OnStatsChanged;

    // Метод для применения маски к игроку
    public void ApplyMask(MaskType mask)
    {
        currentMask = mask; // Установка текущей маски

        // Применяем эффекты маски в зависимости от выбранной маски
        switch (mask)
        {
            case MaskType.Clean:
                healthPoints = 100;
                manaPoints = 100;
                movementSpeed = 5.0f;
                hasPassiveSkill = false;
                break;
            case MaskType.Thief:
                healthPoints = 50;
                manaPoints = 100;
                movementSpeed = 7.5f;
                hasPassiveSkill = true;
                // Дополнительный эффект маски вора
                DropExtraItem();
                break;
            case MaskType.Jester:
                healthPoints = UnityEngine.Random.Range(1, 301);
                manaPoints = UnityEngine.Random.Range(1, 301);
                movementSpeed = UnityEngine.Random.Range(3.0f, 10.0f);
                hasPassiveSkill = false;
                break;
            case MaskType.Chaos:
                // Применить случайную маску
                ApplyMask((MaskType)UnityEngine.Random.Range(0, 3));
                break;
            default:
                break;
        }

        // Вызываем событие обновления статов
        if (OnStatsChanged != null)
            OnStatsChanged.Invoke();
    }

    // Метод для сброса маски к чистой
    public void ResetMask()
    {
        ApplyMask(MaskType.Clean);
    }

    // Дополнительный эффект маски вора
    private void DropExtraItem()
    {
        // Реализация кода для падения дополнительного предмета
        Debug.Log("Extra item dropped!");
    }

    // Вызывается каждый кадр
    private void Update()
    {
        // Вызываем событие обновления статов при изменении маски
        if (Input.GetKeyDown(KeyCode.I))
        {
            ApplyMask(MaskType.Thief); // Пример применения маски по нажатию клавиши
        }
    }
}

// Пример скрипта для обновления интерфейса
public class UIManager : MonoBehaviour
{
    public Text healthText;
    public Text manaText;
    public Text speedText;

    public PlayerStats playerStats;

    private void Start()
    {
        // Подписываемся на событие обновления статов игрока
        playerStats.OnStatsChanged += UpdateStatsUI;
        // Инициализируем UI с текущими статами
        UpdateStatsUI();
    }

    private void UpdateStatsUI()
    {
        healthText.text = "Health: " + playerStats.healthPoints;
        manaText.text = "Mana: " + playerStats.manaPoints;
        speedText.text = "Speed: " + playerStats.movementSpeed;
    }
}
