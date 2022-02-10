using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class AnimatedButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Vector3 onClickScale;
    [SerializeField] private float animationTime;
    [SerializeField] private UnityEvent onClickEvent;
    private bool pointerEnter = false;


    public void OnPointerDown(PointerEventData eventData) => ScaleDown();

    public void OnPointerUp(PointerEventData eventData) => ScaleUP();

    public void OnPointerEnter(PointerEventData eventData) => pointerEnter = true;

    public void OnPointerExit(PointerEventData eventData) => pointerEnter = false;

    private void ScaleDown()
    {
        Tween scaleTween = transform.DOScale(onClickScale, animationTime);
        scaleTween.OnComplete(ExecuteClickEvent);
    }

    private void ScaleUP()
    {
        transform.DOScale(Vector3.one, animationTime);
    }

    private void ExecuteClickEvent()
    {
        if (pointerEnter)
        {
            onClickEvent.Invoke();
        }
    }
}
