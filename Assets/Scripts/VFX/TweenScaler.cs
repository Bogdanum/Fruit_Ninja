using DG.Tweening;
using UnityEngine;

public class TweenScaler : MonoBehaviour
{
   private Tween tween;

   private void OnDestroy()
   {
      CheckTween();
   }

   public void DoScale(Vector3 endValue, float duration, TweenCallback onEnd)
   {
      CheckTween();
      tween = transform.DOScale(endValue, duration);
      tween.onComplete += onEnd;
   }

   public void DoScale(Vector3 endValue, float duration)
   {
      CheckTween();
      tween = transform.DOScale(endValue, duration);
   }

   private void CheckTween()
   {
      if (tween != null)
      {
         tween.Kill(false);
      }
   }
}
