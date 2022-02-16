using UnityEngine;

[CreateAssetMenu(fileName = "DifficultySettings", menuName = "ScriptableObjects/DifficultySettings", order = 7)]
public class DifficultySettings : ScriptableObject
{
    [Range(1, 100)] public float timeToIncreaseDifficulty;
}
