using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerStats : MonoBehaviour
{
    // ����� ������
    public int healthPoints = 100;
    public int manaPoints = 100;
    public float movementSpeed = 5.0f;
    public bool hasPassiveSkill = false;

    // ��������� �����
    public enum MaskType { Clean, Thief, Jester, Chaos }
    public MaskType currentMask = MaskType.Clean;
    public event Action OnStatsChanged;

    // ����� ��� ���������� ����� � ������
    public void ApplyMask(MaskType mask)
    {
        currentMask = mask; // ��������� ������� �����

        // ��������� ������� ����� � ����������� �� ��������� �����
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
                // �������������� ������ ����� ����
                DropExtraItem();
                break;
            case MaskType.Jester:
                healthPoints = UnityEngine.Random.Range(1, 301);
                manaPoints = UnityEngine.Random.Range(1, 301);
                movementSpeed = UnityEngine.Random.Range(3.0f, 10.0f);
                hasPassiveSkill = false;
                break;
            case MaskType.Chaos:
                // ��������� ��������� �����
                ApplyMask((MaskType)UnityEngine.Random.Range(0, 3));
                break;
            default:
                break;
        }

        if (OnStatsChanged != null)
            OnStatsChanged.Invoke();
    }

    // ����� ��� ������ ����� � ������
    public void ResetMask()
    {
        ApplyMask(MaskType.Clean);
    }

    // �������������� ������ ����� ����
    private void DropExtraItem()
    {
        Debug.Log("Extra item dropped!");
    }

    // ����� ��� ��������� �����
    public void TakeDamage(int damage)
    {
        healthPoints -= damage;

        if (healthPoints <= 0)
        {
            Die();
        }

        if (OnStatsChanged != null)
            OnStatsChanged.Invoke();
    }

    // ����� ������
    private void Die()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ApplyMask(MaskType.Thief);
        }
    }
}

public class UIManager : MonoBehaviour
{
    public Text healthText;
    public Text manaText;
    public Text speedText;

    public PlayerStats playerStats;

    private void Start()
    {
        playerStats.OnStatsChanged += UpdateStatsUI;
        UpdateStatsUI();
    }

    private void UpdateStatsUI()
    {
        healthText.text = "Health: " + playerStats.healthPoints;
        manaText.text = "Mana: " + playerStats.manaPoints;
        speedText.text = "Speed: " + playerStats.movementSpeed;
    }
}
