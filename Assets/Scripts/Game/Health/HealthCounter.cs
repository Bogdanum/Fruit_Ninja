using UnityEngine;

public class HealthCounter : MonoBehaviour
{
    [SerializeField] private HealthUI healthUI;
    [SerializeField] private HealthCounterSettings settings;
    private bool isGameOver;
    private int initialHearts;
    private int maxHearts;
    private int currentHeartID;

    private void Awake()
    {
        Init();
        GameplayEvents.TakingDamage.AddListener(RemoveHeart);
        GameplayEvents.Healing.AddListener(AddHeart);
        GameplayEvents.Restart.AddListener(Init);
    }
     private void Init()
     {
         GetSettings();
         healthUI.Init(initialHearts, maxHearts);
         currentHeartID = initialHearts - 1;
         isGameOver = false;

     }
    private void GetSettings()
    {
        initialHearts = settings.initialHealth;
        maxHearts = settings.maxHealth;
    }
    
    private void AddHeart()
    {
        if (currentHeartID != maxHearts)
        {
            currentHeartID++;
            healthUI.AddHeart(currentHeartID, settings.durationOfAppearance);
        }
    }

    private void RemoveHeart()
    {
        if (currentHeartID > 0)
        {
            healthUI.RemoveHeart(currentHeartID, settings.durationOfAppearance);
            currentHeartID--;
            return;
        }
        healthUI.RemoveHeart(0, settings.durationOfAppearance);
        GameOver();
    }

    private void GameOver()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            GameplayEvents.SendGameOverEvent();
        }
    }
}
