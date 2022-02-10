using UnityEngine;

public class VegetableHalf : MonoBehaviour
{
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private PhysicsBody cutVegetablePhysics;
    [SerializeField] private LobuleMovement lobuleMovement;

    private const float DeathlineOffset = 1.5f;
    
    public void Init(VegetableData.VegetableProperties properties, PhysicsBody physics, Sprite sprite, float lobuleSpeed)
   {
       renderer.sprite = sprite;
       renderer.sortingOrder = transform.GetInstanceID();
       gameObject.SetActive(true);
       cutVegetablePhysics.Init(properties.gravity, physics._verticalVelocity, physics._speed, physics._rotationSpeed);
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
