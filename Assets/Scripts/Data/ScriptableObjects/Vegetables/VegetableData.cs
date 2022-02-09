using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "VegetableData", menuName = "ScriptableObjects/VegetableData", order = 1)]
public class VegetableData : ScriptableObject
{
    [System.Serializable]
    public struct VegetableProperties
    {
        public string name;
        public Sprite sprite;
        public Sprite leftHalf;
        public Sprite rightHalf;
        public Sprite splash;
        public Vector2 scale;
        public float gravity;
        public float radius;
        [Range(4, 6)] public float speed;
        [Range(1, 5)] public float lobuleSpeed;
        [Range(1, 20)] public float minRotationSpeed;
        [Range(20, 180)] public float maxRotationSpeed;
        [Range(0, 100)] public float percent;
        public int pointsForDestruction;
        public VegetableTypeEnums.VegetableType vegetableType;
    }

    [SerializeField] private VegetableProperties[] _vegetableProperties;
    
    public VegetableProperties GetRandomVegetableProperties()
    {
        float total = 0;
        float current = 0;

        for (int i = 0; i < _vegetableProperties.Length; i++)
        {
            total += _vegetableProperties[i].percent;
        }

        float randomPercent = Random.Range(0, total);

        for (int i = 0; i < _vegetableProperties.Length; i++)
        {
            current += _vegetableProperties[i].percent;

            if (current >= randomPercent)
            {
                return _vegetableProperties[i];
            }
        }
        return _vegetableProperties[Random.Range(0, _vegetableProperties.Length)];
    }
}
