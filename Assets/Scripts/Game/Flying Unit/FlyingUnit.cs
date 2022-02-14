using UnityEngine;

public class FlyingUnit : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private PhysicsBody physicsBody;
    [SerializeField] private FlyingUnitEffect unitEffect;
    [SerializeField] private ParticleController particleController;
    public bool Active { get; private set; }

    private FlyingUnitData _flyingUnitData;
    private FlyingUnitData.FlyingUnitProperties _properties;
    private ParticleSystem _particleSystem;
    private Vector2 _mousePosition;
    private float _minRotationSpeed;
    private float _maxRotationSpeed;

    private const float DeathlineOffset = 1.5f;

    public void Init(FlyingUnitData flyingUnitData, FlyingUnitData.FlyingUnitProperties properties)
    {
        _flyingUnitData = flyingUnitData;
        _properties = properties;
        spriteRenderer.sprite = properties.sprite;
        spriteRenderer.sortingOrder = transform.GetInstanceID();
        spriteRenderer.sortingLayerName = properties.sortingLayerName;
        _minRotationSpeed = properties.minRotationSpeed;
        _maxRotationSpeed = properties.maxRotationSpeed;
        InstantiateParticleSystem();
        InputEvents.MousePosition.AddListener(SetMousePosition);
    }

    private void InstantiateParticleSystem()
    {
        if (_particleSystem != null)
        {
            Destroy(_particleSystem.gameObject);
        }
        _particleSystem = Instantiate(_properties.particleSystem, FlyingUnitPool.Instance.transform);
        InitParticleController();
    }

    private void InitParticleController()
    {
        particleController.Init(_particleSystem);
        particleController.ChangeParticleColor(_properties.particlesColor);
    }

    private void SetMousePosition(Vector3 position)
    {
        _mousePosition = position;
    }

    public void Launch(float verticalVelocity, float speed, Vector3 startPosition)
    {
        Active = true;
        gameObject.SetActive(true);
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
            physicsBody.Deactivate();
            FlyingUnitPool.Instance.ReturnToPool(this);
            if (_properties.flyingUnitType == FlyingUnitEnums.FlyingUnitType.Fruit)
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
                _particleSystem.transform.position = transform.position;
                unitEffect.PerformEffect(_flyingUnitData, _properties, particleController);
                physicsBody.Deactivate();
                FlyingUnitPool.Instance.ReturnToPool(this);
            }
        }
    }
}