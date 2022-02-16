using UnityEngine;

public class LobuleMovement : MonoBehaviour
{
    private float movementSpeed;
    private float _slowMultiplier = 1f;

    private void Awake()
    {
        GameplayEvents.SlowDownAllUnits.AddListener(SlowDown);
        GameplayEvents.StopGlobalSlowDownEffect.AddListener(SetNormalSpeed);
    }

    public void SetSpeed(float speed)
    {
        if (speed != 0)
        {
            movementSpeed = speed;
        }
    } 
    
    private void Update()
    {
        transform.position += new Vector3(movementSpeed, 0, 0) * Time.deltaTime * _slowMultiplier;
    }

    private void SlowDown(float slowMultiplier)
    {
        _slowMultiplier = slowMultiplier;
    }

    private void SetNormalSpeed()
    {
        _slowMultiplier = 1f;
    }
}
