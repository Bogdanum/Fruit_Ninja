using UnityEngine;
using UnityEngine.Serialization;
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
    [SerializeField] private StorageProvider storageProvider;

    public void Init()
    {
        if (ScoreManager.NewBest)
        {
            newBestScore.SetActive(true);
        }
        accrualAnimator.AccrualAnimation(score, Random.Range(0, ScoreManager.Score), ScoreManager.Score);
        int bestScoreInStorage = LoadBestScore();
        accrualAnimator.AccrualAnimation(bestScore, Random.Range(0, bestScoreInStorage), bestScoreInStorage);
        SetInteractableButtons(true);
    }

    private int LoadBestScore()
    {
        GameData gameData = storageProvider.GetStorage().Load();
        return gameData.BestScore;
    }

    public void InitScale()
    {
        scaleAnimator.InitScale();
    }

    public void Restart()
    {
        SetInteractableButtons(false);
        GameplayEvents.SendRestartEvent();
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
