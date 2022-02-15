using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "FlyingUnitData", menuName = "ScriptableObjects/FlyingUnitData", order = 1)]
public class FlyingUnitData : ScriptableObject
{
    [System.Serializable]
    public struct FlyingUnitProperties
    {
        public string name;
        public FlyingUnitEnums.FlyingUnitType flyingUnitType;
        public string sortingLayerName;
        public Sprite sprite;
        public Color particlesColor;
        public ParticleSystem particleSystem;
        public Vector2 scale;
        public float gravity;
        public float radius;
        [Range(2, 20)] public float speed;
        [Range(1, 20)] public float minRotationSpeed;
        [Range(20, 180)] public float maxRotationSpeed;
        [Range(0, 100)] public float percent;

        [Space(10), Header("Fruits params")]
        public Sprite leftHalf;
        public Sprite rightHalf;
        public Color splashColor;
        [Range(-20, 20)] public float lobuleSpeed;
        public int pointsForDestruction;
        
        [Space(10), Header("Bomb params")]
        public float explosionRadius;
        public float explosionPower;
        
        [Space(10), Header("Pack of fruits params")]
        public Vector2 countOfFruitsInBag;
        public Vector2 fruitsInBagVelocity;
        public Vector2 fruitLaunchAngleFromBag;

        [Space(10), Header("Freeze potion params")]
        public float freezeEffectTime;
        public float slowMultiplier;

        [Space(10), Header("Magnet Params")] 
        public float magnetLifeTime;
        public float magneticFieldRadius;
        public float magnetVelocityMultiplier;
    }

    [SerializeField] private FlyingUnitProperties[] flyingUnitProperties;
    
    public FlyingUnitProperties GetRandomFlyingUnitProperties()
    {
        float total = 0;
        float current = 0;

        for (int i = 0; i < flyingUnitProperties.Length; i++)
        {
            total += flyingUnitProperties[i].percent;
        }

        float randomPercent = Random.Range(0, total);

        for (int i = 0; i < flyingUnitProperties.Length; i++)
        {
            current += flyingUnitProperties[i].percent;

            if (current >= randomPercent)
            {
                return flyingUnitProperties[i];
            }
        }
        return flyingUnitProperties[Random.Range(0, flyingUnitProperties.Length)];
    }

    public FlyingUnitProperties GetRandomFruitProperties()
    {
        float total = 0;
        float current = 0;
        
        for (int i = 0; i < flyingUnitProperties.Length; i++)
        {
            total += flyingUnitProperties[i].percent;
        }

        float randomPercent = Random.Range(0, total);

        for (int i = 0; i < flyingUnitProperties.Length; i++)
        {
            current += flyingUnitProperties[i].percent;

            if (current >= randomPercent)
            {
                return flyingUnitProperties[i].flyingUnitType == FlyingUnitEnums.FlyingUnitType.Fruit ? 
                    flyingUnitProperties[i] : GetRandomFruitProperties();
            }
        }
        var randomProperties = flyingUnitProperties[Random.Range(0, flyingUnitProperties.Length)];
        return randomProperties.flyingUnitType == FlyingUnitEnums.FlyingUnitType.Fruit ? randomProperties : GetRandomFruitProperties();
    }
}
