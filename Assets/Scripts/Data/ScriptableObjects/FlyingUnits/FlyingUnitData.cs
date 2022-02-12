using UnityEngine;

[CreateAssetMenu(fileName = "FlyingUnitData", menuName = "ScriptableObjects/FlyingUnitData", order = 1)]
public class FlyingUnitData : ScriptableObject
{
    [System.Serializable]
    public struct FlyingUnitProperties
    {
        public string name;
        public string sortingLayerName;
        public Sprite sprite;
        public Sprite leftHalf;
        public Sprite rightHalf;
        public Sprite splash;
        public Vector2 scale;
        public float gravity;
        public float radius;
        [Range(2, 20)] public float speed;
        [Range(1, 20)] public float lobuleSpeed;
        [Range(1, 20)] public float minRotationSpeed;
        [Range(20, 180)] public float maxRotationSpeed;
        [Range(0, 100)] public float percent;
        public int pointsForDestruction;
        public float explosionRadius;
        public float explosionPower;
        public FlyingUnitEnums.FlyingUnitType flyingUnitType;
    }

    [SerializeField] private FlyingUnitProperties[] flyingUnitProperties;
    
    public FlyingUnitProperties GetRandomVegetableProperties()
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
}
