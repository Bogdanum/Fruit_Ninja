using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "GameZoneSettings", menuName = "ScriptableObjects/GameplaySettings/GameZoneSettings", order = 4)]
public class GameZoneSettings : ScriptableObject
{
   public float leftWall;
   public float rightWall;
   public float topWall;
   public float bottomWall;
}
