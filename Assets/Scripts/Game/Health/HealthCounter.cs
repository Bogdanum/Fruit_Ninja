using UnityEngine;

public class HealthCounter : MonoBehaviour
{
    [SerializeField] private Heart[] hearts;
    [SerializeField] private HealthCounterSettings settings;
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
       for (int i = 0; i < initialHearts; i++)
       {
           hearts[i].Show(settings.durationOfAppearance);
       }
       currentHeartID = initialHearts - 1;
    }

    private void AddHeart()
    {
        if (currentHeartID != maxHearts)
        {
            currentHeartID++;
            hearts[currentHeartID].Show(settings.durationOfAppearance);
        }
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
