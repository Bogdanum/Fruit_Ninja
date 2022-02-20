using UnityEngine;

[CreateAssetMenu(fileName = "BladeSettings", menuName = "ScriptableObjects/GameplaySettings/BladeSettings", order = 1)]
public class BladeSettings : ScriptableObject
{
   [Range(0.001f, 3)]public float minCuttingVelocity;
}
