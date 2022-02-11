using UnityEngine;
using UnityEngine.UI;

public class RestartPopup : MonoBehaviour
{
    [SerializeField] private Text score;
    [SerializeField] private Text bestscore;
    [SerializeField] private GameObject newBestScore;

    public void Init()
    {
        if (ScoreManager.NewBest)
        {
            newBestScore.SetActive(true);
        }
        score.text = ScoreManager.Score.ToString();
        bestscore.text = PlayerData.BestScore.ToString();
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
