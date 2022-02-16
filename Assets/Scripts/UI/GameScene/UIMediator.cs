using UnityEngine;

public class UIMediator : MonoBehaviour
{
    [SerializeField] private UISettings settings;
    [SerializeField] private FadingPanel gameOverPopupPanel;
    [SerializeField] private RestartPopup restartPopup;
    [SerializeField] private FadingPanel faderPanel;
    [SerializeField] private CanvasGroup faderCanvasGroup;

    private void Awake()
    {
        GameplayEvents.GameOver.AddListener(ShowGameOverPopup);
        InitUI();
    }

    private void InitUI()
    {
        faderCanvasGroup.alpha = 1;
        faderPanel.FadeOut(settings.blackoutDurationOnBoot);
    }
    private void ShowGameOverPopup()
    {
        restartPopup.InitScale();
        gameOverPopupPanel.FadeIn(settings.gameOverPopupFadeInDuration);
    }

    public void HideGameOverPopup()
    {
        gameOverPopupPanel.FadeOut(settings.gameOverPopupFadeInDuration);
    }

    public void FadeInFaderPanel()
    {
        faderPanel.FadeIn(settings.blackoutDurationOnBoot);
    }

    public void LoadHomeScene()
    {
        SceneLoader.LoadSceneAsync(SceneEnums.Scene.HomeScene);
    }
}
