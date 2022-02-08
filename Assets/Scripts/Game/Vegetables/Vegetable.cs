using UnityEngine;

public class Vegetable : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private PhysicsBody physicsBody;
    public bool Active { get; private set; }

    private VegetableData.VegetableProperties _properties;
    private Vector2 _mousePosition;
    private Vector2 _defaultScale;
    private float _minRotationSpeed;
    private float _maxRotationSpeed;

    private const float DeathlineOffset = 1.5f;

    public void Init(VegetableData.VegetableProperties properties)
    {
        _properties = properties;
        spriteRenderer.sprite = properties.sprite;
        _defaultScale = properties.defaultScale;
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
        transform.localScale = _defaultScale;
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
            VegetablePool.Instance.ReturnToPool(this);
            if (_properties.vegetableType == VegetableTypeEnums.VegetableType.Vegetable)
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
                if (_properties.vegetableType == VegetableTypeEnums.VegetableType.Vegetable)
                {
                    SpawnAndInitHulves();
                    SpawnSplash();
                    GameplayEvents.SendPointsIncreaseEvent(_properties.pointsForDestruction);
                }
                VegetablePool.Instance.ReturnToPool(this);
            }
        }
    }

    private void SpawnAndInitHulves()
    {
       var leftHalf = SpawnHalf(_properties.leftHalf,-_properties.lobuleSpeed);
       var rightHalf = SpawnHalf(_properties.rightHalf,_properties.lobuleSpeed);
    }
    
    private void SpawnSplash()
    {
        var splash = SplashPool.Instance.Get();
        splash.transform.position = transform.position;
        splash.Init(_properties.splash);
    }

    private VegetableHalf SpawnHalf(Sprite sprite ,float lobuleSpeed)
    {
        var half = PoolOfHalves.Instance.Get();
        half.transform.localScale = transform.localScale;
        float gravity = _properties.gravity;
        float verticalVelocity = physicsBody._verticalVelocity;
        float speed = physicsBody._speed;
        float rotationSpeed = physicsBody._rotationSpeed;
        half.Init(sprite, gravity, verticalVelocity, speed, rotationSpeed, lobuleSpeed);
        half.transform.position = transform.position;
        half.gameObject.SetActive(true);
        return half;
    }
}
