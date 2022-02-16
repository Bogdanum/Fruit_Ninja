using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class ScaleAnimator : MonoBehaviour
{
    [SerializeField] private Vector3 startScale = Vector3.one;
    [SerializeField] private Ease showEase;
    [SerializeField] private Ease hideEase;
    [SerializeField] private UnityEvent onCompleteShow;
    [SerializeField] private UnityEvent onCompleteHide;
    [SerializeField] private UnityEvent onStartHide;
    private Tween _tween;

    public void InitScale()
    {
        transform.localScale = startScale;
    }

    public void Show(float duration)
    {
        _tween = transform.DOScale(Vector3.one, duration).SetEase(showEase);
        _tween.OnComplete(() =>
        {
            onCompleteShow?.Invoke();
            _tween.Kill();
        });
    }

    public void Hide(float duration)
    {
        onStartHide?.Invoke();
        _tween = transform.DOScale(Vector3.zero, duration).SetEase(hideEase);
        _tween.OnComplete(() =>
        {
            onCompleteHide?.Invoke();
            _tween.Kill();
        });
    }
}

