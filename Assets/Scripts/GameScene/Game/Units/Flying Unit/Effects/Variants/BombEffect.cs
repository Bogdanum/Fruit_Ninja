public class BombEffect : Effect
{
    private BombSettings _settings;
    
    public override void Perform(FlyingUnitSettings settings, ParticleController particleController, BaseFlyingUnit baseFlyingUnit, PhysicsBody physicsBody)
    {
        _settings = (BombSettings)settings;
        this.particleController = particleController;
        this.baseFlyingUnit = baseFlyingUnit;
        this.physicsBody = physicsBody;
        Explosion();
    }

    private void Explosion()
    {
        var explosion = ExplosionsPool.Instance.Get();
        explosion.Init(baseFlyingUnit.transform.position);
        GameplayEvents.SendBombExplosionEvent(baseFlyingUnit.transform.position, _settings.bombParameters.explosionRadius, _settings.bombParameters.explosionForce);
        GameplayEvents.SendTakingDamageEvent();
    }

}