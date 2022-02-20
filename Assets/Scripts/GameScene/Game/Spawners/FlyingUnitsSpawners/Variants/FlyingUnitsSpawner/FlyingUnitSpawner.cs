using System.Collections;
using UnityEngine;

public class FlyingUnitSpawner : Spawner
{
    [SerializeField] private FlyingUnitSpawnerBody flyingUnitSpawnerBody;
    [Space(20),SerializeField] 
    private SpawnerSettings spawnerSettings;

    private int _minPackOfFlyingUnitsSize;
    private int _maxPackOfFlyingUnitsSize;
    private int _complicationCounter = 0;
    private HealthCounter _healthCounter;

    public override int SpawnChanceInPercent => spawnerSettings.spawnChanceInPercent;
    public override SpawnerSettings Settings => spawnerSettings;

    public override void Init(GameZone gameZone, HealthCounter healthCounter)
    {
        flyingUnitSpawnerBody.Init(gameZone);
        _healthCounter = healthCounter;
    }

    private void Awake()
    {
        GetParameters();
        GameplayEvents.IncreasingComplexity.AddListener(IncreaseSizeOfPack);
    }

    private void GetParameters()
    {
        _minPackOfFlyingUnitsSize = spawnerSettings.minPackOfVegetablesSize;
        _maxPackOfFlyingUnitsSize = spawnerSettings.maxPackOfVegetablesSize;
    }

    private void IncreaseSizeOfPack()
    {
        if (_maxPackOfFlyingUnitsSize >= spawnerSettings.finalMaxPackOfVegetablesSize)
        {
            return;
        }
        _complicationCounter++;

        if (_complicationCounter > spawnerSettings.numberOfComplicationsToIncreasePack)
        {
            _complicationCounter = 0;
            _minPackOfFlyingUnitsSize++;
            _maxPackOfFlyingUnitsSize++;
        }
    }

    public override void Launch()
    {
        int packSize = Random.Range(_minPackOfFlyingUnitsSize, _maxPackOfFlyingUnitsSize);
        spawnerSettings.NewPack(packSize);
        StartCoroutine(LaunchRoutine(packSize));
    }

    private IEnumerator LaunchRoutine(int packSize)
    {
        for (int i = 0; i < packSize; i++)
        {
            yield return new WaitForSeconds(spawnerSettings.delayBetweenShotsInPack);
            
            var unit = FlyingUnitPool.Instance.Get();
            InitFlyingUnit(unit);
        }
    }

    private void InitFlyingUnit(BaseFlyingUnit unit)
    {
        var flyingUnitProperties = GetRandomProperties();
        unit.Init(flyingUnitProperties);
        flyingUnitSpawnerBody.SetRandomSpawnPointLinePosition();
        unit.transform.rotation = transform.rotation;
        LaunchFlyingUnit(unit, flyingUnitProperties);
    }

    private FlyingUnitSettings GetRandomProperties()
    {
        var properties = spawnerSettings.GetRandomSettings();
        if (properties.GetType() == typeof(HealthPotionSettings) && _healthCounter.IsFull())
        {
            return GetRandomProperties();
        }
        return properties;
    }

    private void LaunchFlyingUnit(BaseFlyingUnit unit, FlyingUnitSettings settings)
    {
        Vector3 direction = flyingUnitSpawnerBody.GetNormalizedFlightDirection();
        Vector3 velocityVector = direction * settings.physicsParameters.speed;
        unit.Launch(velocityVector, flyingUnitSpawnerBody.SpawnPoint);
    }
}
