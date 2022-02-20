using UnityEngine;

public class ScoreTextSpawner : Singleton<ScoreTextSpawner>
{
    [SerializeField] private ScoreTextSpawnerSettings settings;
    public void SpawnPointsForCutting(Vector3 position, int minPoints, int points)
    {
        var pointsObj = PointsForCuttingPool.Instance.Get();
        pointsObj.transform.position = position;
        pointsObj.transform.rotation = Quaternion.identity;
        int angle = Random.Range(-settings.minAngle, settings.minAngle);
        pointsObj.gameObject.SetActive(true);
        bool combo = points > minPoints ? true : false;
        pointsObj.Init(points, angle, settings.lifeTime, combo);
    }
}
