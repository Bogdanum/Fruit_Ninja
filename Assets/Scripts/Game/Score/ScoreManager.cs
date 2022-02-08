using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static int Score { get; private set; }
    public static bool NewBest { get; private set; }
    private bool gameActive = true;

    private void Awake()
    {
        GameplayEvents.PointsIncrease.AddListener(IncrementScore);
        GameplayEvents.GameOver.AddListener(GameOver);
        NewBest = false;
        Score = 0;
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
        if (Score > PlayerData.BestScore)
        {
            PlayerData.SaveBestScore(Score);
            PlayerData.Refresh();
            UIEvents.SentBestScoreUpdateEvent(PlayerData.BestScore);
            NewBest = true;
        }
    }

    private void GameOver()
    {
        gameActive = false;
    }
}
