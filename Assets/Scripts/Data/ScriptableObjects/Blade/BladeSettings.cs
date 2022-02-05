using UnityEngine;

[CreateAssetMenu(fileName = "BladeSettings", menuName = "ScriptableObjects/BladeSettings", order = 5)]
public class BladeSettings : ScriptableObject
{
   [Range(0.001f, 3)]public float minCuttingVelocity;
}
