using UnityEngine;
using UnityEngine.UI;

public class RestartPopup : MonoBehaviour
{
    [SerializeField] private Text score;
    [SerializeField] private Text bestScore;
    [SerializeField] private AnimatedButton restartButton;
    [SerializeField] private AnimatedButton homeButton;
    [SerializeField] private GameObject newBestScore;
    [SerializeField] private AccrualAnimator accrualAnimator;
    [SerializeField] private ScaleAnimator scaleAnimator;

    public void Init()
    {
        newBestScore.SetActive(ScoreManager.NewBest);
        accrualAnimator.AccrualAnimation(score, Random.Range(0, ScoreManager.Score), ScoreManager.Score);
        accrualAnimator.AccrualAnimation(bestScore, Random.Range(0, ScoreManager.BestScore), ScoreManager.BestScore);
        SetInteractableButtons(true);
    }

    public void InitScale()
    {
        scaleAnimator.InitScale();
    }

    public void Restart()
    {
        SetInteractableButtons(false);
        GameplayEvents.SendRestartEvent();
        newBestScore.SetActive(false);
    }

    public void GoHome()
    {
        SetInteractableButtons(false);
    }

    private void SetInteractableButtons(bool state)
    {
        restartButton.interactable = state;
        homeButton.interactable = state;
    }
}
