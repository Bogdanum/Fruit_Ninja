using UnityEngine;

public class FlyingUnit : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private PhysicsBody physicsBody;
    [SerializeField] private FlyingUnitEffect unitEffect;
    public bool Active { get; private set; }

    private FlyingUnitData.FlyingUnitProperties _properties;
    private Vector2 _mousePosition;
    private float _minRotationSpeed;
    private float _maxRotationSpeed;

    private const float DeathlineOffset = 1.5f;

    public void Init(FlyingUnitData.FlyingUnitProperties properties)
    {
        _properties = properties;
        spriteRenderer.sprite = properties.sprite;
        spriteRenderer.sortingOrder = transform.GetInstanceID();
        spriteRenderer.sortingLayerName = properties.sortingLayerName;
        _minRotationSpeed = properties.minRotationSpeed;
        _maxRotationSpeed = properties.maxRotationSpeed;
        InputEvents.MousePosition.AddListener(SetMousePosition);
    }

    private void SetMousePosition(Vector3 position)
    {
        _mousePosition = position;
    }
    
    public void Launch(float verticalVelocity, float speed, Vector3 startPosition)
    {
        Active = true;
        float rotationSpeed = RandomRotationSpeedAndDirection();
        transform.position = startPosition;
        transform.localScale = _properties.scale;
        physicsBody.Init(_properties.gravity, verticalVelocity, speed, rotationSpeed);
    }

    private float RandomRotationSpeedAndDirection()
    {
        float rotationSpeed = Random.Range(_minRotationSpeed, _maxRotationSpeed);
        int direction = Random.Range(0, 3);
        return direction > 1 ? rotationSpeed : -rotationSpeed;
    }

    public float GetRadius()
    {
        return _properties.radius;
    }

    private void Update()
    {
        if (!Active)
            return;
        
        if (OutOfBounds())
        {
            Active = false;
            FlyingUnitPool.Instance.ReturnToPool(this);
            if (_properties.flyingUnitType == FlyingUnitEnums.FlyingUnitType.Vegetable)
            {
                GameplayEvents.SendTakingDamageEvent();
            }
        }
        CheckCut();
    }

    private bool OutOfBounds()
    {
        return transform.position.y < GameZone.Instance.BottomLine * DeathlineOffset;
    }

    private void CheckCut()
    {
        if (Blade.IsSwipeCut)
        {
            float distance = Vector2.Distance(transform.position, _mousePosition);

            if (distance < _properties.radius)
            {
                unitEffect.PerformEffect(_properties);
                FlyingUnitPool.Instance.ReturnToPool(this);
            }
        }
    }
}