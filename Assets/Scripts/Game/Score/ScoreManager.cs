using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static int Score { get; private set; }

    private void Awake()
    {
        GameplayEvents.PointsIncrease.AddListener(IncrementScore);
    }

    private void IncrementScore(int value)
    {
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
        }
    }
}
