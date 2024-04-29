using UnityEngine;

public class NPCStats : MonoBehaviour
{
    public enum NPCType
    {
        Goblin,
        Skeleton,
    }

    public NPCType npcType;

    public float movementSpeed; // Скорость движения

    public int maxHealth; // Максимальное количество здоровья
    public int damage; // Урон, наносимый NPC

    void Start()
    {
        SetStatsForNPCType(npcType); // Устанавливаем характеристики в момент старта
    }

    public void SetStatsForNPCType(NPCType type)
    {
        switch (type)
        {
            case NPCType.Goblin:
                movementSpeed = 3f;
                maxHealth = 100;
                damage = 0;
                break;
            case NPCType.Skeleton:
                movementSpeed = 2f;
                maxHealth = 80;
                damage = 15;
                break;
        }
    }
}
