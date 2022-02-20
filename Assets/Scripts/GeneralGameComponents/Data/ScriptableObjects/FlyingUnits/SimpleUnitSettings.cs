using UnityEngine;

[CreateAssetMenu(fileName = "SimpleUnitSettings", menuName = "ScriptableObjects/FlyingUnits/SimpleUnitSettings", order = 1)]
public class SimpleUnitSettings : FlyingUnitSettings
{
    [System.Serializable]
    public struct FruitProperties
    {
        public string name;
        public float lobuleSpeed;
        public int pointsForCutting;
    }

    [System.Serializable]
    public struct VisualParameters
    {
        public Sprite sprite;
        public Sprite leftLobule;
        public Sprite rightLobule;
        public ParticleSystem particleSystem;
        public Color juiceColor;
        public Color splashOnWallColor;
    }

    public FruitProperties fruitProperties;
    public VisualParameters visualParameters;
    public override Sprite Sprite => visualParameters.sprite;
}
