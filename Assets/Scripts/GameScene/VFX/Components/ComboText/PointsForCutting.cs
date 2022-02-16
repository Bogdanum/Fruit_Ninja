using System.Collections;
using UnityEngine;
using TMPro;

public class PointsForCutting : MonoBehaviour
{
    [SerializeField] private TMP_Text pointsForCutting;
    [SerializeField] private TweenScaler scaler;
    [SerializeField] private GameObject combo;

    public void Init(int points, int angle, float lifeTime)
    {
        transform.localScale = Vector3.zero;
        StartCoroutine(LifeTime(lifeTime));
        transform.Rotate(new Vector3(0, 0, 1), angle);
        pointsForCutting.text = "+" + points;
    }

    private IEnumerator LifeTime(float seconds)
    {
        DoScale(seconds);
        yield return new WaitForSeconds(seconds);
        PointsForCuttingPool.Instance.ReturnToPool(this);
    }

    private void DoScale(float time)
    {
        scaler.DoScale(Vector3.one, time/2, () =>
        {
            scaler.DoScale(Vector3.zero, time/2);
        });
    }

}
