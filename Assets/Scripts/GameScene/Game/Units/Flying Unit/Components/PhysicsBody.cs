using UnityEngine;

public class PhysicsBody : MonoBehaviour
{
    private bool _isMagnetActive = false;
    private Vector3 _magnetPosition;
    private float _magneticFieldRadius;
    private float _magnetVelocityMultiplier;
    private float _gravity;
    private float _slowMultiplier = 1f;
    public float _verticalVelocity { get; private set; }
    public float _speed { get; private set; }
    public float _rotationSpeed { get; private set; }
    public bool Active {get; private set; }

    private void Awake()
    {
        GameplayEvents.SlowDownAllUnits.AddListener(SlowDown);
        GameplayEvents.StopGlobalSlowDownEffect.AddListener(StopSlowDownEffect);
        GameplayEvents.MagnetEffect.AddListener(SetActiveMagnetParameters);
        GameplayEvents.StopMagnetEffect.AddListener(MagnetDeactivated);
    }

    public void Init(float gravity, float verticalVelocity, float speed, float rotationSpeed)
    {
        _gravity = gravity;
        _verticalVelocity = verticalVelocity;
        _speed = speed;
        _rotationSpeed = rotationSpeed;
        Active = true;
    }

    public void ChangeVelocity(float newVerticalVelocity, float newSpeed)
    {
        _verticalVelocity = newVerticalVelocity;
        _speed = newSpeed;
    }

    public void Activate() => Active = true;
    public void Deactivate() => Active = false;
    
    private void MagnetDeactivated() => _isMagnetActive = false;

    private void Update()
    {
        if (Active)
        {
            GravityMotion();
        }
    }

    private void GravityMotion()
    {
        float deltaTime = Time.deltaTime * _slowMultiplier;
        _verticalVelocity -= _gravity * deltaTime;
        var motionVector = GetMotionVector();
        transform.position += motionVector * deltaTime;
        
        transform.Rotate(new Vector3(0, 0, _rotationSpeed) * deltaTime);
    }

    private Vector3 GetMotionVector()
    {
        if (_isMagnetActive)
        {
            Vector3 distanceVector = _magnetPosition - transform.position;
            if (distanceVector.magnitude < _magneticFieldRadius)
            {
                ChangeVelocity(0, 0);
                var multiplier = distanceVector.magnitude / _magneticFieldRadius;
                return distanceVector.normalized * _magnetVelocityMultiplier * multiplier;
            }
        }
        return new Vector3(_speed, _verticalVelocity, 0);
    }

    private void SlowDown(float slowMultiplier)
    {
        _slowMultiplier = slowMultiplier;
    }

    private void StopSlowDownEffect()
    {
        _slowMultiplier = 1f;
    }

    private void SetActiveMagnetParameters(Vector3 magnetPosition, float magneticFieldRadius, float magnetVelocityMultiplier)
    {
        _magnetPosition = magnetPosition;
        _magneticFieldRadius = magneticFieldRadius;
        _magnetVelocityMultiplier = magnetVelocityMultiplier;
        _isMagnetActive = true;
    }
}
