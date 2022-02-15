using UnityEngine;
using UnityEngine.UI;

public class RestartPopup : MonoBehaviour
{
    [SerializeField] private Text score;
    [SerializeField] private Text bestscore;
    [SerializeField] private GameObject newBestScore;
    [SerializeField] private AccrualAnimator animator;

    public void Init()
    {
        if (ScoreManager.NewBest)
        {
            newBestScore.SetActive(true);
        }
        animator.AccrualAnimation(score, Random.Range(0, ScoreManager.Score), ScoreManager.Score);
        animator.AccrualAnimation(bestscore, Random.Range(0, PlayerData.BestScore), PlayerData.BestScore);
    }

    public void Restart()
    {
        GameplayEvents.SendRestartEvent();
    }

    public void GoHome()
    {
        GameplayEvents.SendRestartEvent();
        SceneLoader.LoadSceneAsync(SceneEnums.Scene.HomeScene);
    }
}
