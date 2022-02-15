using UnityEngine;

public class FlyingUnitHalf : MonoBehaviour
{
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private PhysicsBody physicsBody;
    [SerializeField] private LobuleMovement lobuleMovement;

    private const float DeathlineOffset = 1.5f;

    private void Awake()
    {
        GameplayEvents.Restart.AddListener(ReturnToPool);
    }

    public void Init(FlyingUnitData.FlyingUnitProperties properties, PhysicsBody physics, Sprite sprite, Vector3 startPosition, Vector3 localScale, float lobuleSpeed)
   {
       renderer.sprite = sprite;
       renderer.sortingOrder = transform.GetInstanceID();
       transform.localScale = localScale;
       transform.position = startPosition;
       gameObject.SetActive(true);
       physicsBody.Init(properties.gravity, physics._verticalVelocity, physics._speed, physics._rotationSpeed);
       lobuleMovement.SetSpeed(lobuleSpeed);
   }

    private void Update()
    {
        if (OutOfBounds())
        {
            physicsBody.Deactivate();
            ReturnToPool();
        }
    }
    
    private bool OutOfBounds()
    {
        return transform.position.y < GameZone.Instance.BottomLine * DeathlineOffset;
    }

    private void ReturnToPool()
    {
        PoolOfHalves.Instance.ReturnToPool(this);
    }
}
