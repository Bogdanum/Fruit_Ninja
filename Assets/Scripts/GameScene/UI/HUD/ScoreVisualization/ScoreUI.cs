using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Text bestScoreText;
    [SerializeField] private AccrualAnimator animator;
    [SerializeField] private StorageProvider storageProvider;

    private int currScore;
    private int bestScore;
    private void Awake()
    {
        int bestScoreInStorage = LoadBestScore();
        InitBestScore(bestScoreInStorage);
        UIEvents.OnScoreUpdate.AddListener(UpdateScoreLabel);
        UIEvents.OnBestScoreUpdate.AddListener(UpdateBestScore);
        GameplayEvents.Restart.AddListener(Restart);
    }

    private int LoadBestScore()
    {
        GameData gameData = storageProvider.GetStorage().Load();
        return gameData.BestScore;
    }

    private void InitBestScore(int score)
    {
        bestScoreText.text = score.ToString();
        bestScore = score;
    }

    private void UpdateScoreLabel(int score)
    {
        animator.AccrualAnimation(scoreText, currScore, score);
        currScore = score;
    }

    private void UpdateBestScore(int score)
    {
        animator.AccrualAnimation(bestScoreText, bestScore, score);
        bestScore = score;
    }

    private void Restart()
    {
        currScore = 0;
    }
}
