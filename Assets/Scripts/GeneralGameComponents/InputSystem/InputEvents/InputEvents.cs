using UnityEngine;
using UnityEngine.Events;

public static class InputEvents
{
   public static UnityEvent MouseClickOrTouch = new UnityEvent();
   public static UnityEvent MouseUp = new UnityEvent();
   public static UnityEvent<Vector3> MousePosition = new UnityEvent<Vector3>();

   public static void SendClickEvent()
   {
      MouseClickOrTouch.Invoke();
   }

   public static void SendMouseUpEvent()
   {
      MouseUp.Invoke();
   }

   public static void SendMousePosition(Vector3 position)
   {
      MousePosition.Invoke(position);
   }
}
