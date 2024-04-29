using System;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float HealthPoints { get; set; } = 100.0f;
    public float ManaPoints { get; set; } = 100.0f;
    public float MovementSpeed { get; set; } = 5.0f;
    public bool HasPassiveSkill { get; set; } = false;

    public event Action OnStatsChanged;

    private void Update()
    {
        // Применение маски мага при нажатии клавиши O
        if (Input.GetKeyDown(KeyCode.O))
        {
            ApplyMask(new WizardMask());
        }

        // Перезапуск игры при нажатии клавиши R
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }

        // Пример применения маски вора при нажатии клавиши I
        if (Input.GetKeyDown(KeyCode.I))
        {
            ApplyMask(new ThiefMask());
        }
    }

    public void ApplyMask(IMask mask)
    {
        mask.ApplyMask(this);
        OnStatsChanged?.Invoke();
    }

    // Метод для получения урона
    public void TakeDamage(int damage)
    {
        HealthPoints -= damage;

        if (HealthPoints <= 0)
        {
            Die();
        }

        OnStatsChanged?.Invoke();
    }

    // Логика смерти игрока
    private void Die()
    {
        Debug.Log("Player has died!");

        // Здесь вы можете добавить дополнительную логику для смерти игрока, например, отображение экрана смерти.

        // Перезапуск игры
        RestartGame();
    }

    // Перезапуск игры
    private void RestartGame()
    {
        // Перезапуск текущего уровня
        Application.LoadLevel(Application.loadedLevel);
    }

    // Метод для обновления UI
    public void UpdateStatsUI(Text healthText, Text manaText, Text speedText)
    {
        healthText.text = "Health: " + HealthPoints;
        manaText.text = "Mana: " + ManaPoints;
        speedText.text = "Speed: " + MovementSpeed;
    }
}

// Интерфейс для всех масок
public interface IMask
{
    void ApplyMask(Player player);
}

// Класс маски вора
public class ThiefMask : IMask
{
    public void ApplyMask(Player player)
    {
        player.HealthPoints = 70.0f;
        player.ManaPoints = 100.0f;
        player.MovementSpeed = 7.5f;
        player.HasPassiveSkill = true;
        DropExtraItem();
    }

    private void DropExtraItem()
    {
        Debug.Log("Extra item dropped!");
    }
}

// Класс маски мага
public class WizardMask : IMask
{
    public void ApplyMask(Player player)
    {
        player.HealthPoints = 50.0f;
        player.ManaPoints = 150.0f;
        player.MovementSpeed = 3.0f;
        player.HasPassiveSkill = false;
    }
}