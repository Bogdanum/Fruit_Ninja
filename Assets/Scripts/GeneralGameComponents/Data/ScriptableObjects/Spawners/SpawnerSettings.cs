using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnerSettings", menuName = "ScriptableObjects/GameplaySettings/Spawners/SpawnerSettings", order = 2)]
public class SpawnerSettings : ScriptableObject
{
    public int minPackOfVegetablesSize;
    public int maxPackOfVegetablesSize;
    public int finalMaxPackOfVegetablesSize;
    public int numberOfComplicationsToIncreasePack;
    public FlyingUnitSettings[] simpleFlyingUnitsSettings;
    public FlyingUnitSettings[] bonusesSettings;
    [Range(1, 100)] public int spawnChanceInPercent;
    [Range(0.1f, 3)] public float delayBetweenShotsInPack;

    private int _packSize;
    private Dictionary<FlyingUnitSettings, int> countDict;

     public void NewPack(int packSize)
    {
        _packSize = packSize;
        countDict = new Dictionary<FlyingUnitSettings, int>();
        var allUnitsSettingsArray = simpleFlyingUnitsSettings.Concat(bonusesSettings);
        foreach (var settings in allUnitsSettingsArray)
        {
            countDict.Add(settings, 0);        
        }
    }
     
     public FlyingUnitSettings GetRandomSettings()
    {
        var allUnitsSettingsArray = simpleFlyingUnitsSettings.Concat(bonusesSettings);
        float total = 0;
        float current = 0;

        foreach (var settings in allUnitsSettingsArray)
        {
            total += settings.spawnChanceInPack;
        }
        
        float randomPercent = Random.Range(0, total);

        foreach (var settings in allUnitsSettingsArray)
        {
            current += settings.spawnChanceInPack;

            if (current >= randomPercent)
            {
                return GetPermittedSettings(settings);
            }
        }
        return GetPermittedSettings(allUnitsSettingsArray.First());
    }
    
    
     public FlyingUnitSettings GetPermittedSettings(FlyingUnitSettings settings)
     {
        var percent = countDict[settings] / _packSize;
        if (percent * 100 < settings.maxCountInPackInPercent)
        {
            countDict[settings]++;
            return settings;
        }
        return GetRandomSettings();
     }
     

    public FlyingUnitSettings GetRandomFruitSettings()
    {
        return simpleFlyingUnitsSettings[Random.Range(0, simpleFlyingUnitsSettings.Length)];
    }
}
