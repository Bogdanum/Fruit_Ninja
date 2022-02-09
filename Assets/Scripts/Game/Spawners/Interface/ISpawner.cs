using UnityEngine;

public interface ISpawner
{
   public void Init(GameZone zone);
   public void Launch();
   public int SpawnChanceInPercent { get; }
}
