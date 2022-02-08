using UnityEngine;

[CreateAssetMenu(fileName = "ScoreTextSpawnerSettings", menuName = "ScriptableObjects/ScoreTextSpawnerSettings", order = 9)]
public class ScoreTextSpawnerSettings : ScriptableObject
{
    [Range(-90, 0)] public int minAngle;
    public float lifeTime;
}
