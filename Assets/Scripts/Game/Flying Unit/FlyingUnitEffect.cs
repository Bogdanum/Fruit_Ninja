using UnityEngine;

public class FlyingUnitEffect : MonoBehaviour
{
     [SerializeField] private PhysicsBody physicsBody;
     private FlyingUnitData.FlyingUnitProperties _properties;

     public void PerformEffect(FlyingUnitData.FlyingUnitProperties properties)
     {
          _properties = properties;

          if (_properties.flyingUnitType == FlyingUnitEnums.FlyingUnitType.Vegetable)
          {
               VegetableEffect();
          }
          else if (_properties.flyingUnitType == FlyingUnitEnums.FlyingUnitType.Bomb)
          {
               BombEffect();
          }
     }

     private void VegetableEffect()
     {
          SpawnAndInitHulves();
          SpawnSplash();
          int points = _properties.pointsForDestruction * ComboManager.Instance.GetMultiplier();
          GameplayEvents.SendPointsIncreaseEvent(points);
          ScoreTextSpawner.Instance.SpawnPointsForCutting(transform.position,_properties.pointsForDestruction, points);
     }
     
     private void BombEffect()
     {
          var explosion = ExplosionsPool.Instance.Get();
          explosion.Init(transform.position);
          GameplayEvents.SendBombExplosionEvent(transform.position, _properties.explosionRadius, _properties.explosionPower);
          GameplayEvents.SendTakingDamageEvent();
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
          splash.Init(_properties.splash, transform.position);
     }
}
