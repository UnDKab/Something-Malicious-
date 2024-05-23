using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseClass : MonoBehaviour
{
    public IMask thiefMask; // ����� ����
    public IMask wizardMask; // ����� ����������
    public IMask warriorMask; // ����� �����

    public void ChooseThief()
    {
        // ��������� ����� ���� � ������
        ApplyMask(thiefMask);
        GoToForest();
    }

    public void ChooseWizard()
    {
        // ��������� ����� ���������� � ������
        ApplyMask(wizardMask);
        GoToForest();
    }

    public void ChooseWarrior()
    {
        // ��������� ����� ����� � ������
        ApplyMask(warriorMask);
        GoToForest();
    }

    private void ApplyMask(IMask mask)
    {
        // �������� ������ ������ �� ���������� playerInstance ������ ForestManager
        Player player = ForestManager.playerInstance;
        if (player != null)
        {
            // ��������� ����� � ������
            player.ApplyMask(mask);
        }
        else
        {
            Debug.LogWarning("Player not found in ForestManager.playerInstance.");
        }
    }

    private void GoToForest()
    {
        SceneManager.LoadScene("Forest");
    }
}
