using DG.Tweening;
using UnityEngine;

public class FadingPanel : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    private Tween fadeTween;

    private void OnDestroy()
    {
        CheckTween();
    }

    public void FadeIn(float duration)
    {
        Fade(1, duration, () =>
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        });
    }

    public void FadeOut(float duration)
    {
        Fade(0, duration, () =>
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        });
    }
    
    private void Fade(float endValue, float duration, TweenCallback onEnd)
    {
        CheckTween();
        fadeTween = canvasGroup.DOFade(endValue, duration);
        fadeTween.onComplete += onEnd;
    }

    private void CheckTween()
    {
        if (fadeTween != null)
        {
            fadeTween.Kill(false);
        }
    }
}
