using UnityEngine;

[CreateAssetMenu(fileName = "SpawnerSettings", menuName = "ScriptableObjects/SpawnerSettings", order = 2)]
public class SpawnerSettings : ScriptableObject
{
    public int minPackOfVegetablesSize;
    public int maxPackOfVegetablesSize;
    public VegetableData vegetableData;
    [Range(1, 100)] public int spawnChanceInPercent;
    [Range(0.1f, 3)] public float delayBetweenShotsInPack;
}
