using UnityEngine;

public class PhysicsBody : MonoBehaviour
{
    private bool _active = false;
    private float _gravity;
    private float _slowMultiplier = 1f;
    public float _verticalVelocity { get; private set; }
    public float _speed { get; private set; }
    public float _rotationSpeed { get; private set; }

    private void Awake()
    {
        GameplayEvents.SlowDownAllUnits.AddListener(SlowDown);
        GameplayEvents.StopGlobalSlowDownEffect.AddListener(StopSlowDownEffect);
    }

    public void Init(float gravity, float verticalVelocity, float speed, float rotationSpeed)
    {
        _gravity = gravity;
        _verticalVelocity = verticalVelocity;
        _speed = speed;
        _rotationSpeed = rotationSpeed;
        _active = true;
    }

    public void ChangeVelocity(float newVerticalVelocity, float newSpeed)
    {
        _verticalVelocity = newVerticalVelocity;
        _speed = newSpeed;
    }

    public void Deactivate() => _active = false;

    private void Update()
    {
        if (_active)
        {
            GravityMotion();
        }
    }

    private void GravityMotion()
    {
        float deltaTime = Time.deltaTime * _slowMultiplier;
        _verticalVelocity -= _gravity * deltaTime;
        transform.position += new Vector3(_speed, _verticalVelocity, 0) * deltaTime;
        
        transform.Rotate(new Vector3(0, 0, _rotationSpeed) * deltaTime);
    }

    private void SlowDown(float slowMultiplier)
    {
        _slowMultiplier = slowMultiplier;
    }

    private void StopSlowDownEffect()
    {
        _slowMultiplier = 1f;
    }
    /*
    private void SlowDown(float slowMultiplier, float time)
    {
        if (!_active) return;
        
        _slowMultiplier = slowMultiplier;
        StartCoroutine(TemporarySlowDown(time));
    }

    private IEnumerator TemporarySlowDown(float time)
    {
        yield return new WaitForSeconds(time);
        _slowMultiplier = 1f;
    }
*/
}
