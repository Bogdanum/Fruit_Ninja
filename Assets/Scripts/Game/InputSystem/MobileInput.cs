using UnityEngine;

public class MobileInput : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    private void Update() => ReadTouches();

    private void ReadTouches()
    {
        if (Input.GetMouseButtonDown(0))
        {
            InputEvents.SendClickEvent();
        }

        if (Input.GetMouseButtonUp(0))
        {
            InputEvents.SendMouseUpEvent();
        }
        SendTouchPosition();
    }

    private void SendTouchPosition()
    {
        Vector3 touchPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        InputEvents.SendMousePosition(touchPosition);
    }
}
