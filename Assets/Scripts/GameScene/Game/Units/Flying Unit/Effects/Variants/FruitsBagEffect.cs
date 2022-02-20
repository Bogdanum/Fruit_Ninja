using UnityEngine;
 public class FruitsBagEffect : Effect
 {
     private FruitsBagSettings _settings;
     
     public override void Perform(FlyingUnitSettings settings, ParticleController particleController, BaseFlyingUnit baseFlyingUnit, PhysicsBody physicsBody)
     {
         _settings = (FruitsBagSettings)settings;
         this.particleController = particleController;
         this.baseFlyingUnit = baseFlyingUnit;
         this.physicsBody = physicsBody;
         Effect();
     }
     
     private void Effect()
     {
         int count = (int)Random.Range(_settings.fruitsBagParameters.countOfFruitsInBag.x, _settings.fruitsBagParameters.countOfFruitsInBag.y);
         for (int i = 0; i < count; i++)
         {
             var randomDirection = GetRandomMovementDirection();
             randomDirection *= _settings.fruitsBagParameters.fruitsInBagVelocity;
             SpawnRandomFruit(randomDirection);
         }
     }

     private Vector2 GetRandomMovementDirection()
     {
         var bagParams = _settings.fruitsBagParameters;
         float angle = Random.Range(bagParams.fruitsLaunchAngleFromBag.x, bagParams.fruitsLaunchAngleFromBag.y);
         return Quaternion.Euler(0, 0, angle) * Vector3.right;
     }
     
     private void SpawnRandomFruit(Vector2 direction)
     {
         var randomFruitProperties = SpawnersController.Instance.GetRandomFruitSettings();
         var fruit = FlyingUnitPool.Instance.Get();
         fruit.Init(randomFruitProperties);
         var fruitsBagTransform = baseFlyingUnit.transform;
         fruit.transform.rotation = fruitsBagTransform.rotation;
         var startPosition = new Vector3(fruitsBagTransform.position.x, fruitsBagTransform.position.y + _settings.fruitsBagParameters.fruitsInBagOffsetY, 0);
         fruit.Launch(direction, startPosition);
     }
     
 }
