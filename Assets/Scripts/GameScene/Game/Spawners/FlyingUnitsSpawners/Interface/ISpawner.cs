public interface ISpawner
{
   public void Init(GameZone zone, HealthCounter healthCounter);
   public void Launch();
   public int SpawnChanceInPercent { get; }
}
