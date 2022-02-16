using UnityEngine;

public abstract class Spawner : MonoBehaviour, ISpawner
{
    public virtual void Init(GameZone zone, HealthCounter healthCounter)
    {
    }

    public virtual void Launch()
    {
    }

    public abstract int SpawnChanceInPercent { get; }
}
