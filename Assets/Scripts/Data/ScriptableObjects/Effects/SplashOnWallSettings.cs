using UnityEngine;

[CreateAssetMenu(fileName = "SplashOnWallSettings", menuName = "ScriptableObjects/SplashOnWallSettings", order = 6)]
public class SplashOnWallSettings : ScriptableObject
{
    [Range(0.1f, 10)] public float lifeTime;
    [Range(0.1f, 10)] public float timeForAnimation;
    public Vector2 scale;
}
