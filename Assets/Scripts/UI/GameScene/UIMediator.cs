using UnityEngine;

public class UIMediator : Singleton<UIMediator>
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
        gameOverPopupPanel.FadeIn(settings.gameOverPopupFadeInDuration);
        restartPopup.Init();
    }

    public void FadeInFaderPanel()
    {
        faderPanel.FadeIn(settings.blackoutDurationOnBoot);
    }
}
