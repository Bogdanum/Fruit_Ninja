using UnityEngine;

[CreateAssetMenu(fileName = "BombSettings", menuName = "ScriptableObjects/FlyingUnits/BombSettings", order = 2)]
public class BombSettings : FlyingUnitSettings
{
    [System.Serializable]
    public struct BombParameters
    {
        public float explosionRadius;
        public float explosionForce;
    }
    
    [System.Serializable]
    public struct VisualParameters
    {
        public Sprite sprite;
    }

    public BombParameters bombParameters;
    public VisualParameters visualParameters;

    public override Sprite Sprite => visualParameters.sprite;
}
