using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponData", order = 1)]
public class WeaponModel : MonoBehaviour
{
    public float baseDamage = 10f; // Базовый урон для модели оружия
    public Vector3 pivotLocalPosition;
    public Quaternion pivotLocalRotation;
}
