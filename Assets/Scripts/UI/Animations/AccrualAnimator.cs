using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AccrualAnimator : MonoBehaviour
{
    [SerializeField] private Ease accrualAnimEase;
    [SerializeField] private float animDuration;
    
    public void AccrualAnimation(Text label, float startValue, float endValue)
    {
        var tween = DOVirtual.Float(startValue, endValue, animDuration, score =>
        {
            label.text = $"{score:f0}";
        });
        tween.SetEase(accrualAnimEase).OnComplete(() => tween.Kill());
    }
}
