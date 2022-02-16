using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class AnimatedButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    public bool interactable = true;
    [SerializeField] private Vector3 onClickScale;
    [SerializeField] private float animationTime;
    [SerializeField] private TweenScaler scaler;
    [SerializeField] private UnityEvent onClickEvent;
    private bool _pointerEnter = false;

    public void OnPointerDown(PointerEventData eventData) => ScaleDown();

    public void OnPointerUp(PointerEventData eventData) => ScaleUP();

    public void OnPointerEnter(PointerEventData eventData) => _pointerEnter = true;

    public void OnPointerExit(PointerEventData eventData) => _pointerEnter = false;

    private void ScaleDown()
    {
        scaler.DoScale(onClickScale, animationTime);
    }

    private void ScaleUP()
    {
        ExecuteClickEvent();
        scaler.DoScale(Vector3.one, animationTime);
    }

    private void ExecuteClickEvent()
    {
        if (_pointerEnter && interactable)
        {
            onClickEvent.Invoke();
        }
    }
}
