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
        animator.AccrualAnimation(score, ScoreManager.Score / 2, ScoreManager.Score);
        animator.AccrualAnimation(bestscore, PlayerData.BestScore / 1.5f, PlayerData.BestScore);
    }

    public void Restart()
    {
        SceneLoader.LoadSceneAsync(SceneEnums.Scene.GameScene);
    }

    public void GoHome()
    {
        SceneLoader.LoadSceneAsync(SceneEnums.Scene.HomeScene);
    }
}
