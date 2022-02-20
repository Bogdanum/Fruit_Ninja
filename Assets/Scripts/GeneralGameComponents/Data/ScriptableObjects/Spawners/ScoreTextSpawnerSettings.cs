using UnityEngine;

[CreateAssetMenu(fileName = "ScoreTextSpawnerSettings", menuName = "ScriptableObjects/GameplaySettings/Spawners/ScoreTextSpawnerSettings", order = 3)]
public class ScoreTextSpawnerSettings : ScriptableObject
{
    [Range(0, 90)] public int minAngle;
    public float lifeTime;
}
