using UnityEngine;

public class HealthCounter : MonoBehaviour
{
    [SerializeField] private GameObject[] hearts;
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
           hearts[i].SetActive(true);
       }
       currentHeartID = initialHearts - 1;
    }

    private void AddHeart()
    {
        if (currentHeartID != maxHearts)
        {
            currentHeartID++;
            hearts[currentHeartID].SetActive(true);
        }
    }

    private void RemoveHeart()
    {
        if (currentHeartID > 0)
        {
            hearts[currentHeartID].SetActive(false);
            currentHeartID--;
            return;
        }
        GameOver();
    }

    private void GameOver()
    {
        SceneController.LoadScene(SceneEnums.Scene.GameScene);
    }
}
