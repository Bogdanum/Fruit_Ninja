using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class PointsForCutting : MonoBehaviour
{
    [SerializeField] private TMP_Text pointsForCutting;
    private Tween scaleTween;

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
        if (scaleTween != null)
        {
            scaleTween.Kill(false);
        }
        scaleTween = transform.DOScale(new Vector3(1, 1, 1), time/2);
        scaleTween.onComplete = () =>
        {
            transform.DOScale(new Vector3(0, 0, 0), time/2);
        };
    }
}
