using UnityEngine;

[CreateAssetMenu(fileName = "ComboSettings", menuName = "ScriptableObjects/GameplaySettings/ComboSettings", order = 2)]
public class ComboSettings : ScriptableObject
{
    public int maxMultiplier;
    [Range(0.001f, 1)] public float minDestroyInterval;
}
