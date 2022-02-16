using UnityEngine;

[CreateAssetMenu(fileName = "HealthCounterSettings", menuName = "ScriptableObjects/HealthCounterSettings", order = 7)]
public class HealthCounterSettings : ScriptableObject
{
    public int initialHealth;
    public int maxHealth;
    [Range(0.5f, 3)] public float durationOfAppearance;
}
