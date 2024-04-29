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
        // ���������� ����� ���� ��� ������� ������� O
        if (Input.GetKeyDown(KeyCode.O))
        {
            ApplyMask(new WizardMask());
        }

        // ���������� ���� ��� ������� ������� R
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }

        // ������ ���������� ����� ���� ��� ������� ������� I
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

    // ����� ��� ��������� �����
    public void TakeDamage(int damage)
    {
        HealthPoints -= damage;

        if (HealthPoints <= 0)
        {
            Die();
        }

        OnStatsChanged?.Invoke();
    }

    // ������ ������ ������
    private void Die()
    {
        Debug.Log("Player has died!");

        // ����� �� ������ �������� �������������� ������ ��� ������ ������, ��������, ����������� ������ ������.

        // ���������� ����
        RestartGame();
    }

    // ���������� ����
    private void RestartGame()
    {
        // ���������� �������� ������
        Application.LoadLevel(Application.loadedLevel);
    }

    // ����� ��� ���������� UI
    public void UpdateStatsUI(Text healthText, Text manaText, Text speedText)
    {
        healthText.text = "Health: " + HealthPoints;
        manaText.text = "Mana: " + ManaPoints;
        speedText.text = "Speed: " + MovementSpeed;
    }
}

// ��������� ��� ���� �����
public interface IMask
{
    void ApplyMask(Player player);
}

// ����� ����� ����
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

// ����� ����� ����
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