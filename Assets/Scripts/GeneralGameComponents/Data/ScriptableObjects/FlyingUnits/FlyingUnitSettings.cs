using UnityEngine;

public class FlyingUnitSettings : ScriptableObject
{
    public string sortingLayerName;
    [Range(0, 100)] 
    public int spawnChanceInPack;
    [Range(0, 100)]
    public int maxCountInPackInPercent;
    public Effect unitEffect;
    public PhysicsParameters physicsParameters;

    [System.Serializable]
    public struct PhysicsParameters
    {
        public Vector3 scale;
        public Vector3 maxScale;
        public float speed;
        public float mass;
        public float radius;
        [Range(1, 20)] public float minRotationSpeed;
        [Range(20, 180)] public float maxRotationSpeed;
    }
    
    public virtual Sprite Sprite { get; }
}

