using UnityEngine;

public class Vegetable : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    public bool Active { get; private set; }

    private VegetableData.VegetableProperties _properties;
    private float _speed;
    private float _rotationSpeed;
    private float _verticalVelocity;
    private float _minRotationSpeed;
    private float _maxRotationSpeed;

    public void Init(VegetableData.VegetableProperties properties)
    {
        _properties = properties;
        spriteRenderer.sprite = properties.sprite;
        _minRotationSpeed = properties.minRotationSpeed;
        _maxRotationSpeed = properties.maxRotationSpeed;
    }
    
    public void Launch(float verticalVelocity, float speed, Vector3 startPosition)
    {
        Active = true;
        _verticalVelocity = verticalVelocity;
        _speed = speed;
        _rotationSpeed = RandomRotationSpeedAndDirection();
        transform.position = startPosition;
    }

    private float RandomRotationSpeedAndDirection()
    {
        float rotationSpeed = Random.Range(_minRotationSpeed, _maxRotationSpeed);
        int direction = Random.Range(0, 3);
        return direction > 1 ? rotationSpeed : -rotationSpeed;
    }

    private void Update()
    {
        if (!Active)
            return;
        
        if (OutOfBounds())
        {
            Active = false;
            VegetablePool.Instance.ReturnToPool(this);
        }
        GravityMotion();
    }
    
    private bool OutOfBounds()
    {
        return transform.position.y < GameZone.Instance.BottomLine * 1.5f;
    }

    private void GravityMotion()
    {
        _verticalVelocity -= _properties.gravity * Time.deltaTime;
        transform.position += new Vector3(_speed, _verticalVelocity, 0) * Time.deltaTime;
        
        transform.Rotate(new Vector3(0, 0, _rotationSpeed) * Time.deltaTime);
    }

}
