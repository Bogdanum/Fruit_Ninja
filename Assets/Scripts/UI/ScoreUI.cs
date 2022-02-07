using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Text bestScoreText;

    private void Awake()
    {
        UpdateBestScore(PlayerData.BestScore);
        UIEvents.OnScoreUpdate.AddListener(UpdateScoreLabel);
        UIEvents.OnBestScoreUpdate.AddListener(UpdateBestScore);
    }

    private void UpdateScoreLabel(int score)
    {
        scoreText.text = score.ToString();
        DoScale(scoreText.transform);
    }

    private void UpdateBestScore(int score)
    {
        bestScoreText.text = score.ToString();
        DoScale(bestScoreText.transform);
    }

    private void DoScale(Transform transform)
    {
        var sequence = DOTween.Sequence();
        Tween tween = transform.DOScaleY(0.8f, 0.2f);
        sequence.Append(tween);
        sequence.OnComplete(() => transform.localScale = Vector3.one);
    }
}
