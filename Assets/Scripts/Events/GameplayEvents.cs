using UnityEngine;
using UnityEngine.Events;

public static class GameplayEvents
{
    public static UnityEvent<int> PointsIncrease = new UnityEvent<int>();
    public static UnityEvent<Vector3, float, float> BombExplosion = new UnityEvent<Vector3, float, float>();
    public static UnityEvent IncreasingComplexity = new UnityEvent();
    public static UnityEvent GameOver = new UnityEvent();
    public static UnityEvent TakingDamage = new UnityEvent();
    public static UnityEvent Healing = new UnityEvent();
    public static void SendPointsIncreaseEvent(int value)
    { 
        PointsIncrease.Invoke(value);
    }

    public static void SendBombExplosionEvent(Vector3 bombPosition, float explosionRadius, float power)
    {
        BombExplosion.Invoke(bombPosition, explosionRadius, power);
    }
    
    public static void SendIncreasingComplexityEvent() => IncreasingComplexity.Invoke();
    
    public static void SendGameOverEvent() => GameOver.Invoke();

    public static void SendTakingDamageEvent() => TakingDamage.Invoke();

    public static void SendHealingEvent() => Healing.Invoke();
}
