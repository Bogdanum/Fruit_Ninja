using UnityEngine;

[CreateAssetMenu(fileName = "UISettings", menuName = "ScriptableObjects/UISettings", order = 8)]
public class UISettings : ScriptableObject
{
   [Range(0.1f, 2)] public float gameOverPopupFadeInDuration;
   [Range(0.1f, 3)] public float blackoutDurationOnBoot;
   [Range(0.1f, 5)] public float timeToShowGameOverPopup;
}
