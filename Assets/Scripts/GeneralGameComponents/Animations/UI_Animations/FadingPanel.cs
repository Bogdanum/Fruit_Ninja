using DG.Tweening;
using UnityEditor;
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

    public void FadeIn(float duration, float delay = 0)
    {
        onStartFadeIn?.Invoke();
        fadeTween = canvasGroup.DOFade(1, duration).SetDelay(delay);
        fadeTween.OnComplete(() =>
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            onCompleteFadeIn?.Invoke();
            fadeTween.Kill();
        });
    }

    public void FadeOut(float duration, float delay = 0)
    {
        fadeTween = canvasGroup.DOFade(0, duration).SetDelay(delay);
        fadeTween.OnComplete(() =>
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            onCompleteFadeOut?.Invoke();
            fadeTween.Kill();
        });
    }
    
    private void CheckTween()
    {
        if (fadeTween != null)
        {
            fadeTween.Kill();
        }
    }
}
