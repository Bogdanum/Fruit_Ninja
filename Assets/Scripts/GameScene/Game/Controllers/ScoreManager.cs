using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private StorageProvider storageProvider;
    
    public static int Score { get; private set; }
    public static int BestScore { get; private set; }
    public static bool NewBest { get; private set; }
    private bool gameActive = true;

    private void Awake()
    {
        GameplayEvents.PointsIncrease.AddListener(IncrementScore);
        GameplayEvents.GameOver.AddListener(GameOver);
        GameplayEvents.Restart.AddListener(Restart);
        NewBest = false;
        Score = 0;
        BestScore = LoadBestScore();
    }

    private int LoadBestScore()
    {
        GameData gameData = storageProvider.GetStorage().Load();
        return gameData.BestScore;
    }
    
    private void IncrementScore(int value)
    {
        if (!gameActive)
        {
            return;
        }
        Score += value;
        UIEvents.SendScoreUpdateEvent(Score);
        CheckBestScore();
    }

    private void CheckBestScore()
    {
        if (Score > BestScore)
        {
            BestScore = Score;
            UIEvents.SentBestScoreUpdateEvent(BestScore);
            NewBest = true;
        }
    }

    private void GameOver()
    {
        gameActive = false;
        SaveBestScore(BestScore);
    }

    private void SaveBestScore(int score)
    {
        GameData gameData = new GameData()
        {
            BestScore = score
        };
        storageProvider.GetStorage().Save(gameData);
    }
    
    private void Restart()
    {
        gameActive = true;
        Score = 0;
        NewBest = false;
        UIEvents.SendScoreUpdateEvent(Score);
    }
}
