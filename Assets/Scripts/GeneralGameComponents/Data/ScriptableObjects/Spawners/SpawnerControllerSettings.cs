using UnityEngine;

[CreateAssetMenu(fileName = "SpawnerControllerSettings", menuName = "ScriptableObjects/GameplaySettings/Spawners/SpawnerControllerSettings", order = 1)]
public class SpawnerControllerSettings : ScriptableObject
{
    public float initialRefireRate;
    public float minRefireRate;
    public float refireRateReductionStep;
}
