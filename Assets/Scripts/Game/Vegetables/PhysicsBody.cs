using UnityEngine;

public class PhysicsBody : MonoBehaviour
{
    private bool _active = false;
    private float _gravity;
    public float _verticalVelocity { get; private set; }
    public float _speed { get; private set; }
    public float _rotationSpeed { get; private set; }
    
    public void Init(float gravity, float verticalVelocity, float speed, float rotationSpeed)
    {
        _gravity = gravity;
        _verticalVelocity = verticalVelocity;
        _speed = speed;
        _rotationSpeed = rotationSpeed;
        _active = true;
    }
    
    private void Update()
    {
        if (_active)
        {
            GravityMotion();
        }
    }

    private void GravityMotion()
    {
        _verticalVelocity -= _gravity * Time.deltaTime;
        transform.position += new Vector3(_speed, _verticalVelocity, 0) * Time.deltaTime;

        transform.localScale += new Vector3(_verticalVelocity, _verticalVelocity, 0) * Time.deltaTime * 0.5f;
        transform.Rotate(new Vector3(0, 0, _rotationSpeed) * Time.deltaTime);
    }
    
}
