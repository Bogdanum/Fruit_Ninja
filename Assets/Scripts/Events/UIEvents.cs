using UnityEngine.Events;

public static class UIEvents
{
    public static UnityEvent<int> OnScoreUpdate = new UnityEvent<int>();
    public static UnityEvent<int> OnBestScoreUpdate = new UnityEvent<int>();

    public static void SendScoreUpdateEvent(int value)
    {
        OnScoreUpdate.Invoke(value);
    }

    public static void SentBestScoreUpdateEvent(int value)
    {
        OnBestScoreUpdate.Invoke(value);
    }
}
