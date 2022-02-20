using UnityEngine;

public abstract class Effect : MonoBehaviour
{
     protected ParticleController particleController;
     protected BaseFlyingUnit baseFlyingUnit;
     protected PhysicsBody physicsBody;

     public abstract void Perform(FlyingUnitSettings settings, ParticleController particleController, BaseFlyingUnit baseFlyingUnit, PhysicsBody physicsBody);
     public virtual void End(PhysicsBody physicsBody) {}

     public virtual void OutOfBounds(BaseFlyingUnit unit) => ReturnToPool(unit);

     public virtual void ReturnToPool(BaseFlyingUnit unit)
     {
          FlyingUnitPool.Instance.ReturnToPool(unit);
     }
}
