using UnityEngine;

[CreateAssetMenu(fileName = "MagnetSettings", menuName = "ScriptableObjects/FlyingUnits/MagnetSettings", order = 6)]
public class MagnetSettings : FlyingUnitSettings
{
    [System.Serializable]
    public struct MagnetParameters
    {
        public float magnetLifeTime;
        public float magneticFieldRadius;
        public float magnetVelocityMultiplier;
    }
    
    [System.Serializable]
    public struct VisualParameters
    {
        public Sprite sprite;
        public ParticleSystem particleSystem;
        public Color particlesColor;
    }
    
    public MagnetParameters magnetParameters;
    public VisualParameters visualParameters;
    public override Sprite Sprite => visualParameters.sprite;
}
