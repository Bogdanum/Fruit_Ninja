using UnityEngine;

public class MobileInput : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    private void Update() => ReadTouches();

    private void ReadTouches()
    {
        SendTouchPosition();
        
        if (Input.GetMouseButtonDown(0))
        {
            InputEvents.SendClickEvent();
        }

        if (Input.GetMouseButtonUp(0))
        {
            InputEvents.SendMouseUpEvent();
        }
    }

    private void SendTouchPosition()
    {
#if UNITY_EDITOR
        Vector3 touchPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
#else
        Vector3 touchPosition = _camera.ScreenToWorldPoint(Input.GetTouch(0).position);
#endif
        InputEvents.SendMousePosition(touchPosition);
    }
}
