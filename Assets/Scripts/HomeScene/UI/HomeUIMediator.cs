using UnityEngine;
using UnityEngine.UI;

public class HomeUIMediator : MonoBehaviour
{
    [SerializeField] private UISettings settings;
    [SerializeField] private StorageProvider storageProvider;
    [SerializeField] private FadingPanel faderPanel;
    [SerializeField] private CanvasGroup faderCG;
    [SerializeField] private Text bestScore;

    private void Awake()
    {
        InitUI();
    }

    private void InitUI()
    {
        bestScore.text = LoadBestScore();

        faderCG.alpha = 1f;
        faderPanel.FadeOut(settings.blackoutDurationOnBoot);
    }

    private string LoadBestScore()
    {
        GameData gameData = storageProvider.GetStorage().Load();
        return gameData.BestScore.ToString();
    }

    public void StartGame()
    {
        faderPanel.FadeIn(settings.blackoutDurationOnBoot);
    }

    public void LoadGameScene()
    {
        SceneLoader.LoadScene(SceneEnums.Scene.GameScene);
    }
}
