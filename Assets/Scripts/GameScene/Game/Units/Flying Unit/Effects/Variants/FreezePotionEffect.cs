using UnityEngine;

public class FreezePotionEffect : Effect
{
    private FreezePotionSettings _settings;
    private ParticleSystem _particleSystem;

    public override void Perform(FlyingUnitSettings settings, ParticleController particleController, BaseFlyingUnit baseFlyingUnit, PhysicsBody physicsBody)
    {
        _settings = (FreezePotionSettings)settings;
        this.particleController = particleController;
        this.baseFlyingUnit = baseFlyingUnit;
        this.physicsBody = physicsBody;
        InstantiateParticleSystem();
        FreezeEffect();
    }
    
    private void InstantiateParticleSystem()
    {
        _particleSystem = Instantiate(_settings.visualParameters.particleSystem, FlyingUnitPool.Instance.transform);
        _particleSystem.transform.position = baseFlyingUnit.transform.position;
        InitParticleController();
    }

    private void InitParticleController()
    {
        particleController.Init(_particleSystem);
        particleController.ChangeParticleProperties(_settings.visualParameters.particlesColor);
    }
    
    private void FreezeEffect()
    {
        particleController.Play();
        GameplayEvents.SendFreezePotionEvent(_settings.freezeParameters.slowMultiplier, _settings.freezeParameters.freezeEffectTime);
    }
    
}
