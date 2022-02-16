using UnityEngine;

[ExecuteInEditMode]
public class SpawnerVisualization : MonoBehaviour
{
#if UNITY_EDITOR

   [SerializeField] private Transform minLinePos;
   [SerializeField] private Transform maxLinePos;
   [SerializeField] private Transform minAngle;
   [SerializeField] private Transform maxAngle;
   [SerializeField] private Transform center;
   [SerializeField] private Color linesColor;
   
   private void Update()
   {
      DisplaySpawnRange();
      DisplayMinLaunchAngle();
      DisplayMaxLaunchAngle();
   }

   private void DisplaySpawnRange()
   {
      Debug.DrawLine(minLinePos.position, maxLinePos.position, linesColor);
   }

   private void DisplayMinLaunchAngle()
   {
      Debug.DrawLine(center.position, minAngle.position, linesColor);
   }
   
   private void DisplayMaxLaunchAngle()
   {
      Debug.DrawLine(center.position, maxAngle.position, linesColor);
   }
   
#endif
}
