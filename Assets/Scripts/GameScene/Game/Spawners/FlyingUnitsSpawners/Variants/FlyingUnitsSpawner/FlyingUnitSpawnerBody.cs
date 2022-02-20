using UnityEngine;

public class FlyingUnitSpawnerBody : MonoBehaviour
{
    [SerializeField] private Transform minLinePos;
    [SerializeField] private Transform maxLinePos;
    [SerializeField] private Transform minAngle;
    [SerializeField] private Transform maxAngle;
    [SerializeField] private Transform center;
    private GameZone _gameZone;

    public Vector3 SpawnPoint => center.position;
    
    public void Init(GameZone gameZone)
    {
        _gameZone = gameZone;
        CorrectPositionX();
    }
    
    private void CorrectPositionX()
    {
        float newX = transform.position.x * _gameZone.CameraAspect;
        transform.position = new Vector3(newX, transform.position.y, 0);
        float minPosX = minLinePos.localPosition.x * _gameZone.CameraAspect;
        float maxPosX = maxLinePos.localPosition.x * _gameZone.CameraAspect;
        if (minPosX != 0 && maxPosX != 0)
        {
            minLinePos.localPosition = new Vector3(minPosX, minLinePos.localPosition.y, 0);
            maxLinePos.localPosition = new Vector3(maxPosX, maxLinePos.localPosition.y, 0);
        }
    }

    public void SetRandomSpawnPointLinePosition()
    {
        float x = Random.Range(minLinePos.position.x, maxLinePos.position.x);
        float y = Random.Range(minLinePos.position.y, maxLinePos.position.y);
        center.position = new Vector3(x, y, 1);
    }
    
    public Vector3 GetNormalizedFlightDirection()
    {
        Vector3 minAngleDir = minAngle.position - center.position;
        Vector3 maxAngleDir = maxAngle.position - center.position;

        return new Vector3(Random.Range(minAngleDir.x, maxAngleDir.x), Random.Range(minAngleDir.y, maxAngleDir.y), 0).normalized;
    }
    
    
}