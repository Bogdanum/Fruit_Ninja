using System.Collections;
using UnityEngine;

public class VegetableSpawner : Spawner, ISpawner
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
    private VegetableData _vegetableData;
    private VegetableData.VegetableProperties _vegetableProperties;

    public int SpawnChanceInPercent { get => spawnerSettings.spawnChanceInPercent; }

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

    public void Launch()
    {
        int packSize = Random.Range(_minPackOfVegetablesSize, _maxPackOfVegetablesSize);
        StartCoroutine(LaunchRoutine(packSize));
    }

    private IEnumerator LaunchRoutine(int packSize)
    {
        for (int i = 0; i < packSize; i++)
        {
            yield return new WaitForSeconds(spawnerSettings.delayBetweenShotsInPack);
            
            var vegetable = VegetablePool.Instance.Get();
            InitVegetable(vegetable);
            LaunchVegetable(vegetable);
        }
    }

    private void InitVegetable(Vegetable vegetable)
    {
        _vegetableProperties = _vegetableData.GetRandomVegetableProperties();
        vegetable.Init(_vegetableProperties);
        center.position = GetRandomLinePosition();
        vegetable.transform.rotation = transform.rotation;
        vegetable.transform.position = center.position;
        vegetable.transform.SetAsLastSibling();
        vegetable.gameObject.SetActive(true);
    }

    private Vector3 GetRandomLinePosition()
    {
        float x = Random.Range(minLinePos.position.x, maxLinePos.position.x);
        float y = Random.Range(minLinePos.position.y, maxLinePos.position.y);
        return new Vector3(x, y, 0);
    }
    
    private void LaunchVegetable(Vegetable vegetable)
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
