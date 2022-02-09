using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthCounter : MonoBehaviour
{
    [SerializeField] private Heart heart;
    [SerializeField] private GridLayoutGroup grid;
    [SerializeField] private HealthCounterSettings settings;
    private Heart[] hearts;
    private int initialHearts;
    private int maxHearts;
    private int currentHeartID;

    private void Awake()
    {
        GetSettings();
        Init();
        GameplayEvents.TakingDamage.AddListener(RemoveHeart);
        GameplayEvents.Healing.AddListener(AddHeart);
    }

    private void GetSettings()
    {
        initialHearts = settings.initialHealth;
        maxHearts = settings.maxHealth;
    }

    private void Init()
    {
        hearts = new Heart[maxHearts]; 
        for (int i = 0; i < maxHearts; i++)
        {
           hearts[i] = Instantiate(heart, transform);
           if (i < initialHearts)
           {
               hearts[i].Show(settings.durationOfAppearance);
           }
        }
        currentHeartID = initialHearts - 1;
        SetGridSize(initialHearts);
    }

    private void AddHeart()
    {
        if (currentHeartID != maxHearts)
        {
            SetGridSize(currentHeartID);
            currentHeartID++;
            hearts[currentHeartID].Show(settings.durationOfAppearance);
        }
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

    private void RemoveHeart()
    {
        if (currentHeartID > 0)
        {
            hearts[currentHeartID].Hide(settings.durationOfAppearance);
            currentHeartID--;
            return;
        }
        hearts[0].Hide(settings.durationOfAppearance);
        GameOver();
    }

    private void GameOver()
    {
        GameplayEvents.SendGameOverEvent();
    }
}
