using UnityEngine;

[CreateAssetMenu(fileName = "FreezePotionSettings", menuName = "ScriptableObjects/FlyingUnits/FreezePotionSettings", order = 5)]
public class FreezePotionSettings : FlyingUnitSettings
{
    [System.Serializable]
    public struct FreezeParameters
    {
        public float freezeEffectTime;
        public float slowMultiplier;
    }
    
    [System.Serializable]
    public struct VisualParameters
    {
        public Sprite sprite;
        public ParticleSystem particleSystem;
        public Color particlesColor;
    }
    
    public FreezeParameters freezeParameters;
    public VisualParameters visualParameters;

    public override Sprite Sprite => visualParameters.sprite;
}
