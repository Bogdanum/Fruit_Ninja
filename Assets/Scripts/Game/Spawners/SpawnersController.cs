using UnityEngine;

public class SpawnersController : MonoBehaviour
{
   [SerializeField] private float minRefireRate;
   [SerializeField] private GameObject[] spawnersObjects;
   
   private ISpawner[] _spawners;
   private float fireTimer = 0;

   private void Awake()
   {
      _spawners = new ISpawner[spawnersObjects.Length];
      for (int i = 0; i < spawnersObjects.Length; i++)
      {
         _spawners[i] = spawnersObjects[i].GetComponent<ISpawner>();
      }
   }

   private void Update()
   {
      if (_spawners.Length == 0)
      {
         return;
      }
      
      fireTimer += Time.deltaTime;

      if (fireTimer > minRefireRate)
      {
         fireTimer = 0;
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
