using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class FadingPanel : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private UnityEvent onStartFadeIn;
    [SerializeField] private UnityEvent onCompleteFadeIn;
    [SerializeField] private UnityEvent onCompleteFadeOut;
    private Tween fadeTween;

    private void OnDestroy()
    {
        CheckTween();
    }

    public void FadeIn(float duration)
    {
        onStartFadeIn?.Invoke();
        Fade(1, duration, () =>
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            onCompleteFadeIn?.Invoke();
        });
    }

    public void FadeOut(float duration)
    {
        Fade(0, duration, () =>
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            onCompleteFadeOut?.Invoke();
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
