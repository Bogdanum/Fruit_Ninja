using UnityEngine;

public class FlyingUnitEffectReceiver : MonoBehaviour
{
    [SerializeField] private PhysicsBody physicsBody;

    private void Awake()
    {
        GameplayEvents.BombExplosion.AddListener(BlastWave);
    }

    private void BlastWave(Vector3 bombPosition, float explosionRadius, float power)
    {
        if (!physicsBody.Active) return;

        var direction = transform.position - bombPosition;
        float vectorLength = direction.magnitude;
        if (vectorLength <= explosionRadius)
        {
            var forceVector = CalculateExplosionEffect(direction, vectorLength, explosionRadius, power);
            float newVerticalVelocity = forceVector.y;
            float newSpeed = forceVector.x;
            physicsBody.ChangeVelocity(newVerticalVelocity, newSpeed);
        }
    }

    private Vector2 CalculateExplosionEffect(Vector3 direction, float vectorLength, float radius, float power)
    {
        float velocityEffector = vectorLength / radius;
        var forceVector = (direction.normalized * power * velocityEffector);
        return forceVector;
    }
}
