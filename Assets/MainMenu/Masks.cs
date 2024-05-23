using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseClass : MonoBehaviour
{
    public IMask thiefMask; // Маска вора
    public IMask wizardMask; // Маска волшебника
    public IMask warriorMask; // Маска воина

    public void ChooseThief()
    {
        // Применяем маску вора к игроку
        ApplyMask(thiefMask);
        GoToForest();
    }

    public void ChooseWizard()
    {
        // Применяем маску волшебника к игроку
        ApplyMask(wizardMask);
        GoToForest();
    }

    public void ChooseWarrior()
    {
        // Применяем маску воина к игроку
        ApplyMask(warriorMask);
        GoToForest();
    }

    private void ApplyMask(IMask mask)
    {
        // Получаем объект игрока из переменной playerInstance класса ForestManager
        Player player = ForestManager.playerInstance;
        if (player != null)
        {
            // Применяем маску к игроку
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
