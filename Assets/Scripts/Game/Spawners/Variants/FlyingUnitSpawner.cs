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
    
    private int _minPackOfVegetablesSize;
    private int _maxPackOfVegetablesSize;
    private int _complicationCounter = 0;
    private GameZone _zone;
    private FlyingUnitData _vegetableData;
    private FlyingUnitData.FlyingUnitProperties _vegetableProperties;

    public override int SpawnChanceInPercent { get => spawnerSettings.spawnChanceInPercent; }
    
    public override void Init(GameZone zone)
    {
        _zone = zone;
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
        _minPackOfVegetablesSize = spawnerSettings.minPackOfVegetablesSize;
        _maxPackOfVegetablesSize = spawnerSettings.maxPackOfVegetablesSize;
        _vegetableData = spawnerSettings.vegetableData;
    }

    private void IncreaseSizeOfPack()
    {
        if (_maxPackOfVegetablesSize >= spawnerSettings.finalMaxPackOfVegetablesSize)
        {
            return;
        }
        _complicationCounter++;

        if (_complicationCounter > spawnerSettings.numberOfComplicationsToIncreasePack)
        {
            _complicationCounter = 0;
            _minPackOfVegetablesSize++;
            _maxPackOfVegetablesSize++;
        }
    }

    public override void Launch()
    {
        int packSize = Random.Range(_minPackOfVegetablesSize, _maxPackOfVegetablesSize);
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
        _vegetableProperties = _vegetableData.GetRandomVegetableProperties();
        vegetable.Init(_vegetableProperties);
        center.position = GetRandomLinePosition();
        vegetable.transform.rotation = transform.rotation;
        vegetable.transform.position = center.position;
        vegetable.gameObject.SetActive(true);
    }

    private Vector3 GetRandomLinePosition()
    {
        float x = Random.Range(minLinePos.position.x, maxLinePos.position.x);
        float y = Random.Range(minLinePos.position.y, maxLinePos.position.y);
        return new Vector3(x, y, 0);
    }
    
    private void LaunchVegetable(FlyingUnit vegetable)
    {
        Vector3 flightDirection = GetFlightDirection().normalized * _vegetableProperties.speed;
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