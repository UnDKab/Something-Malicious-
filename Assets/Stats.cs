using System.Collections;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private float healthPoints = 100.0f;
    private float maxHealthPoints = 100.0f;
    private float manaPoints = 100.0f;
    private float movementSpeed = 5.0f;
    private bool hasPassiveSkill = false;
    private int runCount = 0; // Переменная для отслеживания номера текущего забега

    public float HealthPoints
    {
        get => healthPoints;
        set
        {
            healthPoints = value;
            UpdateHealthBar();
            InvokeStatsChanged();
        }
    }

    public float MaxHealthPoints
    {
        get => maxHealthPoints;
        set
        {
            maxHealthPoints = value;
            InvokeStatsChanged();
        }
    }

    public float ManaPoints
    {
        get => manaPoints;
        set
        {
            manaPoints = value;
            InvokeStatsChanged();
        }
    }

    public float MovementSpeed
    {
        get => movementSpeed;
        set
        {
            movementSpeed = value;
            InvokeStatsChanged();
        }
    }

    public bool HasPassiveSkill
    {
        get => hasPassiveSkill;
        set
        {
            hasPassiveSkill = value;
            InvokeStatsChanged();
        }
    }

    public Image hpBar; // Ссылка на Image HP бара

    public event Action OnStatsChanged;

    protected void InvokeStatsChanged()
    {
        OnStatsChanged?.Invoke();
    }

    private void Start()
    {
        UpdateHealthBar();
    }

    public void ApplyMask(IMask mask)
    {
        mask.ApplyMask(this);
        UpdateHealthBar();
    }

    // Метод для получения урона
    public void TakeDamage(int damage)
    {
        HealthPoints -= damage;
        if (HealthPoints <= 0)
        {
            HealthPoints = 0;
            Die();
        }
    }

    // Метод для лечения
    public void Heal(int amount)
    {
        HealthPoints += amount;
        if (HealthPoints > MaxHealthPoints)
        {
            HealthPoints = MaxHealthPoints;
        }
    }

    // Логика смерти игрока
    private void Die()
    {
        runCount++; // Увеличиваем номер текущего забега при смерти игрока
        Debug.Log($"Player has died! Current run: {runCount}"); // Выводим сообщение о номере текущего забега

        // Переход на сцену MainMenu
        StartCoroutine(GoToMainMenu());
    }

    // Переход на сцену MainMenu
    private IEnumerator GoToMainMenu()
    {
        yield return new WaitForSeconds(1); // Можно добавить задержку для завершения всех операций
        SceneManager.LoadScene("MainMenu");

        // Убедитесь, что курсор видим и не заблокирован
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Метод для обновления HP бара
    private void UpdateHealthBar()
    {
        if (hpBar != null)
        {
            float fillAmount = HealthPoints / MaxHealthPoints;
            Debug.Log($"Updating HP bar: HealthPoints = {HealthPoints}, MaxHealthPoints = {MaxHealthPoints}, fillAmount = {fillAmount}");
            hpBar.fillAmount = fillAmount;
        }
        else
        {
            Debug.LogWarning("HP bar is not assigned.");
        }
    }
}

public interface IMask
{
    void ApplyMask(Player player);
}

public class ThiefMask : IMask
{
    public void ApplyMask(Player player)
    {
        player.MaxHealthPoints = 70.0f;
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

public class WizardMask : IMask
{
    public void ApplyMask(Player player)
    {
        player.MaxHealthPoints = 50.0f;
        player.HealthPoints = 50.0f;
        player.ManaPoints = 150.0f;
        player.MovementSpeed = 5.0f;
        player.HasPassiveSkill = false;
    }
}

public class WarriorMask : IMask
{
    public void ApplyMask(Player player)
    {
        player.MaxHealthPoints = 200.0f;
        player.HealthPoints = 200.0f;
        player.ManaPoints = 50.0f;
        player.MovementSpeed = 2.5f;
        player.HasPassiveSkill = false;
    }
}
