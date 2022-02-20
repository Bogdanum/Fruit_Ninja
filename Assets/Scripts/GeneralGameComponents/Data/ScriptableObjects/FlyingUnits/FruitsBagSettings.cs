using UnityEngine;

[CreateAssetMenu(fileName = "FruitsBagSettings", menuName = "ScriptableObjects/FlyingUnits/FruitsBagSettings", order = 4)]
public class FruitsBagSettings : FlyingUnitSettings
{
   [System.Serializable]
   public struct FruitsBagParameters
   {
      public Vector2 countOfFruitsInBag;
      public Vector2 fruitsInBagVelocity;
      public Vector2 fruitsLaunchAngleFromBag;
      public float fruitsInBagOffsetY;
   }
   
   [System.Serializable]
   public struct VisualParameters
   {
      public Sprite sprite;
   }

   public FruitsBagParameters fruitsBagParameters;
   public VisualParameters visualParameters;

   public override Sprite Sprite => visualParameters.sprite;
}
