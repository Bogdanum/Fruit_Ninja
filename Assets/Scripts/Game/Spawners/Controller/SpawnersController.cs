using UnityEngine;

public class SpawnersController : MonoBehaviour
{
   [SerializeField] private SpawnerControllerSettings _settings;
   [SerializeField] private Spawner[] spawners;
   
   private ISpawner[] _spawners;
   private float _fireTimer = 0;
   private float _initialrefireRate;
   private float _minRefireRate;
   private float _refireRateReductionStep;

   private void Awake()
   {
      Init();
      InitSpawners();
      GameplayEvents.IncreasingComplexity.AddListener(DecreaseRefireRate);
   }

   private void Init()
   {
      _initialrefireRate = _settings.initialRefireRate;
      _minRefireRate = _settings.minRefireRate;
      _refireRateReductionStep = _settings.refireRateReductionStep;
   }

   private void InitSpawners()
   {
      _spawners = new ISpawner[spawners.Length];
      for (int i = 0; i < spawners.Length; i++)
      {
         _spawners[i] = (ISpawner)spawners[i];
      }
   }

   private void DecreaseRefireRate()
   {
      if (_initialrefireRate - _minRefireRate < _refireRateReductionStep)
      {
         return;
      }
      _initialrefireRate -= _refireRateReductionStep;
   }

   private void Update()
   {
      if (_spawners.Length == 0)
      {
         return;
      }
      
      _fireTimer += Time.deltaTime;

      if (_fireTimer > _initialrefireRate)
      {
         _fireTimer = 0;
         ISpawner spawner = ChooseRandomSpawner();
         spawner.Launch();
      } 
   }

   private ISpawner ChooseRandomSpawner()
   {
      float total = 0;
      float current = 0;

      for (int i = 0; i < _spawners.Length; i++)
      {
         total += _spawners[i].SpawnChanceInPercent;
      }

      float randomPercent = Random.Range(0, total);

      for (int i = 0; i < _spawners.Length; i++)
      {
         current += _spawners[i].SpawnChanceInPercent;

         if (current >= randomPercent)
         {
            return _spawners[i];
         }
      }
      return _spawners[Random.Range(0, _spawners.Length)];
   }
}
