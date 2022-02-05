using UnityEngine;

public interface ISpawner
{
   public void Launch();
   public int SpawnChanceInPercent { get; }
}
