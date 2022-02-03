using UnityEngine;

[CreateAssetMenu(fileName = "SpawnerControllerSettings", menuName = "ScriptableObjects/SpawnerControllerSettings", order = 3)]
public class SpawnerControllerSettings : ScriptableObject
{
    public float initialRefireRate;
    public float minRefireRate;
    public float refireRateReductionStep;
}
