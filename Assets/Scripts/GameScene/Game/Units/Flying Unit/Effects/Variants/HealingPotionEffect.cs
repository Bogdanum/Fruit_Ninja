using UnityEngine;

public class HealingPotionEffect: Effect
{
    private HealthPotionSettings _settings;
    private ParticleSystem _particleSystem;
        
    public override void Perform(FlyingUnitSettings settings, ParticleController particleController, BaseFlyingUnit baseFlyingUnit, PhysicsBody physicsBody)
    {
        _settings = (HealthPotionSettings)settings;
        this.particleController = particleController;
        this.baseFlyingUnit = baseFlyingUnit;
        this.physicsBody = physicsBody;
        InstantiateParticleSystem();
        HealingEffect();
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

    private void HealingEffect()
    {
        particleController.Play();
        GameplayEvents.SendHealingEvent();
    }
    
}
