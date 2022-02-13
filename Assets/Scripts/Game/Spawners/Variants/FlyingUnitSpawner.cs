using System.Collections;
using UnityEngine;

public class FlyingUnitSpawner : Spawner
{
    [SerializeField] private Transform minLinePos;
    [SerializeField] private Transform maxLinePos;
    [SerializeField] private Transform minAngle;
    [SerializeField] private Transform maxAngle;
    [SerializeField] private Transform center;
    [Space(20),SerializeField] 
    private SpawnerSettings spawnerSettings;

    private int _minPackOfFlyingUnitsSize;
    private int _maxPackOfFlyingUnitsSize;
    private int _complicationCounter = 0;
    private GameZone _zone;
    private HealthCounter _healthCounter;
    private FlyingUnitData _flyingUnitData;
    private FlyingUnitData.FlyingUnitProperties _flyingUnitProperties;

    public override int SpawnChanceInPercent { get => spawnerSettings.spawnChanceInPercent; }
    
    public override void Init(GameZone zone, HealthCounter healthCounter)
    {
        _zone = zone;
        _healthCounter = healthCounter;
        CorrectPositionX();
    }

    private void CorrectPositionX()
    {
        float newX = transform.position.x * _zone.CameraAspect;
        transform.position = new Vector3(newX, transform.position.y, 0);
        float minPosX = minLinePos.localPosition.x * _zone.CameraAspect;
        float maxPosX = maxLinePos.localPosition.x * _zone.CameraAspect;
        if (minPosX != 0 && maxPosX != 0)
        {
            minLinePos.localPosition = new Vector3(minPosX, minLinePos.localPosition.y, 0);
            maxLinePos.localPosition = new Vector3(maxPosX, maxLinePos.localPosition.y, 0);
        }
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
        _flyingUnitData = spawnerSettings.vegetableData;
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
        StartCoroutine(LaunchRoutine(packSize));
    }

    private IEnumerator LaunchRoutine(int packSize)
    {
        for (int i = 0; i < packSize; i++)
        {
            yield return new WaitForSeconds(spawnerSettings.delayBetweenShotsInPack);
            
            var vegetable = FlyingUnitPool.Instance.Get();
            InitVegetable(vegetable);
            LaunchVegetable(vegetable);
        }
    }

    private void InitVegetable(FlyingUnit vegetable)
    {
        _flyingUnitProperties = GetRandomProperties();
        vegetable.Init(_flyingUnitProperties);
        center.position = GetRandomLinePosition();
        vegetable.transform.rotation = transform.rotation;
        vegetable.transform.position = center.position;
        vegetable.gameObject.SetActive(true);
    }

    private FlyingUnitData.FlyingUnitProperties GetRandomProperties()
    {
        var properties = _flyingUnitData.GetRandomVegetableProperties();
        if (properties.flyingUnitType == FlyingUnitEnums.FlyingUnitType.HealingPotion && _healthCounter.IsFull())
        {
            return GetRandomProperties();
        }
        return properties;
    }

    private Vector3 GetRandomLinePosition()
    {
        float x = Random.Range(minLinePos.position.x, maxLinePos.position.x);
        float y = Random.Range(minLinePos.position.y, maxLinePos.position.y);
        return new Vector3(x, y, 0);
    }
    
    private void LaunchVegetable(FlyingUnit vegetable)
    {
        Vector3 flightDirection = GetFlightDirection().normalized * _flyingUnitProperties.speed;
        float verticalVelocity = flightDirection.y;
        float speed = flightDirection.x;
        vegetable.Launch(verticalVelocity, speed, center.position);
    }

    private Vector3 GetFlightDirection()
    {
        Vector3 minAngleDir = minAngle.position - center.position;
        Vector3 maxAngleDir = maxAngle.position - center.position;

        return new Vector3(Random.Range(minAngleDir.x, maxAngleDir.x), Random.Range(minAngleDir.y, maxAngleDir.y), 0);
    }
}
