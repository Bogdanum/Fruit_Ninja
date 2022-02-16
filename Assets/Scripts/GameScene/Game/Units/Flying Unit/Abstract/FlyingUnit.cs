using System.Collections;
using UnityEngine;

public class FlyingUnit : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private PhysicsBody physicsBody;
    [SerializeField] private FlyingUnitEffect unitEffect;
    [SerializeField] private ParticleController particleController;
    public bool Active { get; private set; }
    public bool FreeFall { get; private set; }

    private FlyingUnitData _flyingUnitData;
    private FlyingUnitData.FlyingUnitProperties _properties;
    private ParticleSystem _particleSystem;
    private Vector2 _mousePosition;
    private float _minRotationSpeed;
    private float _maxRotationSpeed;

    private const float DeathlineOffset = 1.5f;

    private void Awake()
    {
        GameplayEvents.Restart.AddListener(() => ReturnToPool(false));
    }

    public void Init(FlyingUnitData flyingUnitData, FlyingUnitData.FlyingUnitProperties properties)
    {
        _flyingUnitData = flyingUnitData;
        _properties = properties;
        spriteRenderer.sprite = properties.sprite;
        spriteRenderer.sortingOrder = transform.GetInstanceID();
        spriteRenderer.sortingLayerName = properties.sortingLayerName;
        _minRotationSpeed = properties.minRotationSpeed;
        _maxRotationSpeed = properties.maxRotationSpeed;
        FreeFall = false;
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
        if (_properties.flyingUnitType == FlyingUnitEnums.FlyingUnitType.Magnet)
        {
            particleController.ChangeParticleProperties(_properties.particlesColor, _properties.magnetLifeTime - 1);
        }
        else
        {
            particleController.ChangeParticleProperties(_properties.particlesColor);
        }
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
            ReturnToPool(false);
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
        if (FreeFall) return;
        
        if (Blade.IsSwipeCut)
        {
            float distance = Vector2.Distance(transform.position, _mousePosition);

            if (distance < _properties.radius)
            {
                _particleSystem.transform.position = transform.position;
                unitEffect.PerformEffect(_flyingUnitData, _properties, particleController);
                physicsBody.Deactivate();
                bool hasDelay = _properties.flyingUnitType == FlyingUnitEnums.FlyingUnitType.Magnet;
                ReturnToPool(hasDelay);
            }
        }
    }

    private void ReturnToPool(bool hasDelay)
    {
        if (hasDelay)
        {
            StartCoroutine(ReturnToPoolWithDelay(_properties.magnetLifeTime)); 
        }
        else
            FlyingUnitPool.Instance.ReturnToPool(this);
    }

    private IEnumerator ReturnToPoolWithDelay(float delay)
    {
        FreeFall = true;
        yield return new WaitForSeconds(delay);
        unitEffect.EndMagnetEffect();
    }
}