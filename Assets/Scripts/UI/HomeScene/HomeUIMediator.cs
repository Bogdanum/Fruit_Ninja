using UnityEngine;
using UnityEngine.UI;

public class HomeUIMediator : MonoBehaviour
{
    [SerializeField] private UISettings settings;
    [SerializeField] private FadingPanel faderPanel;
    [SerializeField] private CanvasGroup faderCG;
    [SerializeField] private Text bestScore;

    private void Awake()
    {
        InitUI();
    }

    private void InitUI()
    {
        bestScore.text = PlayerData.BestScore.ToString();

        faderCG.alpha = 1f;
        faderPanel.FadeOut(settings.blackoutDurationOnBoot);
    }

    public void StartGame()
    {
        faderPanel.FadeIn(settings.blackoutDurationOnBoot);
        SceneLoader.LoadSceneAsync(SceneEnums.Scene.GameScene);
    }
}
