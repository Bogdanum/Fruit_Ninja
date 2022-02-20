using UnityEngine;

[CreateAssetMenu(fileName = "HealthPotionSettings", menuName = "ScriptableObjects/FlyingUnits/HealthPotionSettings", order = 3)]
public class HealthPotionSettings : FlyingUnitSettings
{
    [System.Serializable]
    public struct VisualParameters
    {
        public Sprite sprite;
        public ParticleSystem particleSystem;
        public Color particlesColor;
    }

    public VisualParameters visualParameters;
    public override Sprite Sprite => visualParameters.sprite;
}
