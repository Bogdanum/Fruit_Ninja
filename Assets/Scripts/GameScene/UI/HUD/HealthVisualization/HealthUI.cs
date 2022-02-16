using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Heart heart;
    [SerializeField] private GridLayoutGroup grid;
    private Heart[] hearts;

    public void Init(int initialHearts, int maxHearts)
    {
        hearts = new Heart[maxHearts];
        for (int i = 0; i < maxHearts; i++)
        {
            hearts[i] = Instantiate(heart, transform);
            if (i < initialHearts)
            {
                hearts[i].Show();
            }
        }
        SetGridSize(initialHearts);
    }

    public void AddHeart(int currentHeartID, float durationOfAppearance)
    {
        SetGridSize(currentHeartID + 1);
        hearts[currentHeartID].Show(durationOfAppearance);
    }
    
    public void RemoveHeart(int heartIdToRemove, float durationOfAppearance)
    {
        hearts[heartIdToRemove].Hide(durationOfAppearance);
        SetGridSize(heartIdToRemove);
    }
    
    private void SetGridSize(int visibleHeartsCount)
    {
        var gridSizes = PlayerData.HealthGridSizes;
        foreach (var kv in gridSizes)
        {
            if (visibleHeartsCount < kv.Key)
            {
                grid.cellSize = new Vector2(gridSizes[kv.Key], gridSizes[kv.Key]);
                return;
            }
        }
        grid.cellSize = new Vector2(5, 5);
    }
}
