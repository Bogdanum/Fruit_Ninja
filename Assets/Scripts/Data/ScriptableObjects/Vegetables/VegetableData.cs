using UnityEngine;

[CreateAssetMenu(fileName = "VegetableData", menuName = "ScriptableObjects/VegetableData", order = 1)]
public class VegetableData : ScriptableObject
{
    [System.Serializable]
    public struct VegetableProperties
    {
        public string name;
        public Sprite sprite;
        public float gravity;
        [Range(4, 6)] public float speed;
        [Range(0, 100)] public float percent;
        public Type type;
        public enum Type
        {
            Vegetable,
            Bomb,
            ExtraLife,
            StringBag,
            Freeze,
            Magnet
        }
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
