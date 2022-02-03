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

   private void Update()
   {
      DisplaySpawnRange();
      DisplayMinLaunchAngle();
      DisplayMaxLaunchAngle();
   }

   private void DisplaySpawnRange()
   {
      maxLinePos.localPosition = new Vector3(Mathf.Abs(minLinePos.localPosition.x), Mathf.Abs(minLinePos.localPosition.y), 0);
      Debug.DrawLine(minLinePos.position, maxLinePos.position, Color.green);
   }

   private void DisplayMinLaunchAngle()
   {
       Debug.DrawLine(center.position, minAngle.position, Color.green);
   }
   
   private void DisplayMaxLaunchAngle()
   {
      Debug.DrawLine(center.position, maxAngle.position, Color.green);
   }
   
#endif
}
