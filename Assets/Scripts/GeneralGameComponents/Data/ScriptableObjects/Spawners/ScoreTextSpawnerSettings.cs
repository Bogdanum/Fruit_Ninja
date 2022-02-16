using UnityEngine;

[CreateAssetMenu(fileName = "ScoreTextSpawnerSettings", menuName = "ScriptableObjects/ScoreTextSpawnerSettings", order = 9)]
public class ScoreTextSpawnerSettings : ScriptableObject
{
    [Range(0, 90)] public int minAngle;
    public float lifeTime;
}
