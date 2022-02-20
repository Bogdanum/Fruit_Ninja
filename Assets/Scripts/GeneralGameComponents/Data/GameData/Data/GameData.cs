using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public int BestScore;
    
    public readonly Dictionary<int, float> HealthGridSizes = new Dictionary<int, float>()
    {
        {8, 100},
        {32, 50},
        {128, 25},
        {512, 12.5f},
        {2048, 6.25f}
    };
}
