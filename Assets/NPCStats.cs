using UnityEngine;

public class NPCStats : MonoBehaviour
{
    public enum NPCType
    {
        Goblin,
        Skeleton,
    }

    public NPCType npcType;

    public float movementSpeed; // �������� ��������

    public int maxHealth; // ������������ ���������� ��������
    public int damage; // ����, ��������� NPC

    void Start()
    {
        SetStatsForNPCType(npcType); // ������������� �������������� � ������ ������
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
