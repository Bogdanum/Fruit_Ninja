using UnityEngine;

public class SimpleUnitEffect : Effect
{
    private SimpleUnitSettings _concreteSettings;
    private ParticleSystem _particleSystem;
    
    public override void Perform(FlyingUnitSettings settings, ParticleController particleController, BaseFlyingUnit baseFlyingUnit, PhysicsBody physicsBody)
    {
        _concreteSettings = (SimpleUnitSettings)settings;
        this.particleController = particleController;
        this.baseFlyingUnit = baseFlyingUnit;
        this.physicsBody = physicsBody;
        
        InstantiateParticleSystem();
        VisualEffect();
    }

    private void InstantiateParticleSystem()
    {
        _particleSystem = Instantiate(_concreteSettings.visualParameters.particleSystem, FlyingUnitPool.Instance.transform);
        _particleSystem.transform.position = baseFlyingUnit.transform.position;
        InitParticleController();
    }

    private void InitParticleController()
    {
        particleController.Init(_particleSystem);
        particleController.ChangeParticleProperties(_concreteSettings.visualParameters.juiceColor);
    }
    
    private void VisualEffect()
    {
        SpawnAndInitHulves();
        SpawnSplash();
        int points = _concreteSettings.fruitProperties.pointsForCutting * ComboManager.Instance.GetMultiplier();
        GameplayEvents.SendPointsIncreaseEvent(points);
        ScoreTextSpawner.Instance.SpawnPointsForCutting(baseFlyingUnit.transform.position,_concreteSettings.fruitProperties.pointsForCutting, points);
    }
     
    private void SpawnAndInitHulves()
    {
        var visualProperties = _concreteSettings.visualParameters;
        var leftHalf = SpawnHalf(visualProperties.leftLobule,-_concreteSettings.fruitProperties.lobuleSpeed);
        var rightHalf = SpawnHalf(visualProperties.rightLobule,_concreteSettings.fruitProperties.lobuleSpeed);
    }
     
    private FlyingUnitHalf SpawnHalf(Sprite sprite, float lobuleSpeed)
    {
        var half = PoolOfHalves.Instance.Get();
        var fruitTransform = baseFlyingUnit.transform;
        half.Init(physicsBody, _concreteSettings.physicsParameters.mass, sprite, fruitTransform.position,  fruitTransform.localScale, lobuleSpeed);
        return half;
    }
     
    private void SpawnSplash()
    {
        var splash = SplashPool.Instance.Get();
        splash.Init(_concreteSettings.visualParameters.juiceColor, baseFlyingUnit.transform.position);
        particleController.Play();
    }

    public override void OutOfBounds(BaseFlyingUnit unit)
    {
        GameplayEvents.SendTakingDamageEvent();
        base.ReturnToPool(unit);
    }
    
}