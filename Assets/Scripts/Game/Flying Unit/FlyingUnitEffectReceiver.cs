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
        float effector = CalculateExplosionEffect(bombPosition, explosionRadius, power);
        float newVerticalVelocity = physicsBody._verticalVelocity * effector;
        float newSpeed = physicsBody._speed * effector;
        physicsBody.ChangeVelocity(newVerticalVelocity, newSpeed);
    }

    private float CalculateExplosionEffect(Vector3 bombPosition, float radius, float power)
    {
        float unitPosX = transform.position.x - bombPosition.x;
        float unitPosY = transform.position.y - bombPosition.y;
        float unitPoint = Mathf.Sqrt(Mathf.Pow(unitPosX, 2) + Mathf.Pow(unitPosY, 2));
        if (unitPoint <= radius)
        {
            float velocityEffector = (radius - unitPoint) / radius * power;
            return velocityEffector;
        }
        return 1;
    }
}
