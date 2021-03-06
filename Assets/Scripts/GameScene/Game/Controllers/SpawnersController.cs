using UnityEngine;

public class SpawnersController : Singleton<SpawnersController>
{
   [SerializeField] private SpawnerControllerSettings _settings;
   [SerializeField] private Spawner[] spawners;
   [SerializeField] private GameZone zone;
   [SerializeField] private HealthCounter healthCounter;
   
   private ISpawner[] _spawners;
   private float _fireTimer = 0;
   private float _initialrefireRate;
   private float _minRefireRate;
   private float _refireRateReductionStep;
   private bool active = true;

   protected override void Awake()
   {
      base.Awake();
      Init();
      InitSpawners();
      GameplayEvents.IncreasingComplexity.AddListener(DecreaseRefireRate);
      GameplayEvents.GameOver.AddListener(Stop);
      GameplayEvents.Restart.AddListener(Restart);
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
         _spawners[i].Init(zone, healthCounter);
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

   private void Stop() => active = false;

   private void Restart()
   {
      active = true;
      Init();
   }

   private void Update()
   {
      if (_spawners.Length == 0 || !active)
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

   public FlyingUnitSettings GetRandomFruitSettings()
   {
      return spawners[0].Settings.GetRandomFruitSettings();
   }
}
