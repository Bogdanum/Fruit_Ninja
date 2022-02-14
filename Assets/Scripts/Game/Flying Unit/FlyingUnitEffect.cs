using UnityEngine;
using System.Collections;

public class FlyingUnitEffect : MonoBehaviour
{
     [SerializeField] private PhysicsBody physicsBody;
     private FlyingUnitData _flyingUnitData;
     private FlyingUnitData.FlyingUnitProperties _properties;
     private ParticleController _particleController;

     public void PerformEffect(FlyingUnitData flyingUnitData, FlyingUnitData.FlyingUnitProperties properties, ParticleController particleController)
     {
          _flyingUnitData = flyingUnitData;
          _properties = properties;
          _particleController = particleController;

          switch (_properties.flyingUnitType)
          {
               case FlyingUnitEnums.FlyingUnitType.Fruit:
               {
                    VegetableEffect();
                    break;
               }
               case FlyingUnitEnums.FlyingUnitType.Bomb:
               {
                    BombEffect();
                    break;
               }
               case FlyingUnitEnums.FlyingUnitType.HealingPotion:
               {
                    HealingPotionEffect();
                    break;
               }
               case FlyingUnitEnums.FlyingUnitType.FruitsBag:
               {
                    FruitsBagEffect();
                    break;
               }
               case FlyingUnitEnums.FlyingUnitType.FreezePotion:
               {
                    FreezePotionEffect();
                    break;
               }
          }
     }

     private void VegetableEffect()
     {
          SpawnAndInitHulves();
          SpawnSplash();
          int points = _properties.pointsForDestruction * ComboManager.Instance.GetMultiplier();
          GameplayEvents.SendPointsIncreaseEvent(points);
          if (points > _properties.pointsForDestruction)
          {
               ScoreTextSpawner.Instance.SpawnPointsForCutting(transform.position, points);
          }
     }
     
     private void SpawnAndInitHulves()
     {
          var leftHalf = SpawnHalf(_properties.leftHalf,-_properties.lobuleSpeed);
          var rightHalf = SpawnHalf(_properties.rightHalf,_properties.lobuleSpeed);
     }
     
     private FlyingUnitHalf SpawnHalf(Sprite sprite, float lobuleSpeed)
     {
          var half = PoolOfHalves.Instance.Get();
          half.Init(_properties, physicsBody, sprite, transform.position, transform.localScale, lobuleSpeed);
          return half;
     }
     
     private void SpawnSplash()
     {
          var splash = SplashPool.Instance.Get();
          splash.Init(_properties.splashColor, transform.position);
          _particleController.Play();
     }
     
     private void BombEffect()
     {
          var explosion = ExplosionsPool.Instance.Get();
          explosion.Init(transform.position);
          GameplayEvents.SendBombExplosionEvent(transform.position, _properties.explosionRadius, _properties.explosionPower);
          GameplayEvents.SendTakingDamageEvent();
     }
     
     private void HealingPotionEffect()
     {
          _particleController.Play();
          GameplayEvents.SendHealingEvent();
     }
     
     private void FruitsBagEffect()
     {
          int count = (int)Random.Range(_properties.countOfFruitsInBag.x, _properties.countOfFruitsInBag.y);
          for (int i = 0; i < count; i++)
          {
               var randomDirection = GetRandomMovementDirection();
               randomDirection *= _properties.fruitsInBagVelocity;
               SpawnRandomFruit(randomDirection);
          }
     }

     private Vector2 GetRandomMovementDirection()
     {
          float angle = Random.Range(_properties.fruitLaunchAngleFromBag.x, _properties.fruitLaunchAngleFromBag.y);
          return Quaternion.Euler(0, 0, angle) * Vector3.right;
     }
     
     private void SpawnRandomFruit(Vector2 direction)
     {
          var randomFruitProperties = _flyingUnitData.GetRandomFruitProperties();
          var fruit = FlyingUnitPool.Instance.Get();
          fruit.Init(_flyingUnitData, randomFruitProperties);
          fruit.transform.rotation = transform.rotation;
          fruit.Launch(direction.y, direction.x, transform.position);
     }
     
     private void FreezePotionEffect()
     {
          _particleController.Play();
          GameplayEvents.SendFreezePotionEvent(_properties.slowMultiplier, _properties.freezeEffectTime);
     }
}
