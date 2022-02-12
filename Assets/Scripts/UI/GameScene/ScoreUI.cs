using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Text bestScoreText;
    [SerializeField] private AccrualAnimator animator;

    private int currScore;
    private int bestScore;
    private void Awake()
    {
        InitBestScore(PlayerData.BestScore);
        UIEvents.OnScoreUpdate.AddListener(UpdateScoreLabel);
        UIEvents.OnBestScoreUpdate.AddListener(UpdateBestScore);
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
}
