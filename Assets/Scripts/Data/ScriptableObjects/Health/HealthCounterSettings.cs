using UnityEngine;

[CreateAssetMenu(fileName = "HealthCounterSettings", menuName = "ScriptableObjects/HealthCounterSettings", order = 7)]
public class HealthCounterSettings : ScriptableObject
{
    [Range(1, 5)] public int initialHealth;
    [Range(1, 5)] public int maxHealth;
}
