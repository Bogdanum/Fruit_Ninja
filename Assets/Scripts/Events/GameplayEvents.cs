using UnityEngine.Events;

public static class GameplayEvents
{
    public static UnityEvent IncreasingComplexity = new UnityEvent();

    public static void SendIncreasingComplexityEvent() => IncreasingComplexity.Invoke();
}
