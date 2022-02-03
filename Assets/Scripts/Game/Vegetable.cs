using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Vegetable : MonoBehaviour
{
    public bool Active { get; private set; }
    
    private VegetableData.VegetableProperties _properties;
    private float _speed;
    private float _rotationSpeed;
    private float _verticalVelocity;

    public void Init(VegetableData.VegetableProperties properties)
    {
        _properties = properties;
        var spriteRend = GetComponent<SpriteRenderer>();
        spriteRend.sprite = _properties.sprite;
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
        float rotationSpeed =  Random.Range(20, 150);
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
        return transform.position.y < -CameraBorders.Border;
    }

    private void GravityMotion()
    {
        _verticalVelocity -= _properties.gravity * Time.deltaTime;
        transform.position += new Vector3(_speed, _verticalVelocity, 0) * Time.deltaTime;
        
        transform.Rotate(new Vector3(0, 0, _rotationSpeed) * Time.deltaTime);
    }

}
