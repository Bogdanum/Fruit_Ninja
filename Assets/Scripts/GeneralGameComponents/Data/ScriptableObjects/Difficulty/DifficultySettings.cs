using UnityEngine;

[CreateAssetMenu(fileName = "DifficultySettings", menuName = "ScriptableObjects/GameplaySettings/DifficultySettings", order = 3)]
public class DifficultySettings : ScriptableObject
{
    [Range(1, 100)] public float timeToIncreaseDifficulty;
}
