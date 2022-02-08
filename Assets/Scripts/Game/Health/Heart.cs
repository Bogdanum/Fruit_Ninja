using UnityEngine;
using DG.Tweening;

public class Heart : MonoBehaviour
{
    private Tween scaleTween;
    
    public void Show(float duration)
    {
        gameObject.SetActive(true);
        transform.localScale = Vector3.zero;
        SmoothAppearance(duration);
    }

    private void SmoothAppearance(float duration)
    {
        CheckTween();
        scaleTween = transform.DOScale(Vector3.one, duration);
    }

    public void Hide(float duration)
    {
        CheckTween();
        scaleTween = transform.DOScale(Vector3.zero, duration);
        scaleTween.onComplete += () =>
        {
            gameObject.SetActive(false);
        };
    }

    private void CheckTween()
    {
        if (scaleTween != null)
        {
            scaleTween.Kill(false);
        }
    }
}
