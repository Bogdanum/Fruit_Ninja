using UnityEngine;

public class MagnetEffect : Effect
{
    private MagnetSettings _settings;
    private ParticleSystem _particleSystem;
    
    public override void Perform(FlyingUnitSettings settings, ParticleController particleController, BaseFlyingUnit baseFlyingUnit, PhysicsBody physicsBody)
    {
        _settings = (MagnetSettings)settings;
        this.particleController = particleController;
        this.baseFlyingUnit = baseFlyingUnit;
        this.physicsBody = physicsBody;
        InstantiateParticleSystem();
        Effect();
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
        particleController.ChangeParticleProperties(_settings.visualParameters.particlesColor, _settings.magnetParameters.magnetLifeTime - 1);
    }

    private void Effect()
    {
        particleController.Play();
        var magnetParams = _settings.magnetParameters;
        GameplayEvents.SendMagnetEffectEvent(baseFlyingUnit.transform.position, magnetParams.magneticFieldRadius, magnetParams.magnetVelocityMultiplier);
    }

    public override void OutOfBounds(BaseFlyingUnit unit)
    {
        base.ReturnToPool(unit);
    }

    public override void ReturnToPool(BaseFlyingUnit unit)
    {
        unit.FreeFall = true;
        unit.EndEffectWithDelay(_settings.magnetParameters.magnetLifeTime);
    }

    public override void End(PhysicsBody physics)
    {
        GameplayEvents.SendStopMagneticEffect();
        physics.ChangeVelocity(0, 0);
        physics.Activate();
    }
}
