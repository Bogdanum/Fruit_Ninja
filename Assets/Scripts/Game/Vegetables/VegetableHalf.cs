using UnityEngine;

public class VegetableHalf : MonoBehaviour
{
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private PhysicsBody cutVegetablePhysics;
    [SerializeField] private LobuleMovement lobuleMovement;

    private const float DeathlineOffset = 1.5f;

    public void Init(Sprite sprite, float gravity, float verticalVelocity, float speed,
        float rotationSpeed, float lobuleSpeed)
    {
        renderer.sprite = sprite;
        renderer.sortingOrder = transform.GetInstanceID();
        cutVegetablePhysics.Init(gravity, verticalVelocity, speed, rotationSpeed);
        lobuleMovement.SetSpeed(lobuleSpeed);
    }

    private void Update()
    {
        if (OutOfBounds())
        {
            PoolOfHalves.Instance.ReturnToPool(this);
        }
    }
    
    private bool OutOfBounds()
    {
        return transform.position.y < GameZone.Instance.BottomLine * DeathlineOffset;
    }
}
