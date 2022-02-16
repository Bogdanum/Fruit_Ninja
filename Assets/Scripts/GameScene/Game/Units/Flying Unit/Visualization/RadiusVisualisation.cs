using UnityEngine;

public class RadiusVisualisation : MonoBehaviour
{
#if  UNITY_EDITOR
    [SerializeField] private FlyingUnit unit;
    
    [SerializeField] private bool Visible = false;
    [SerializeField] private Color lineColor = Color.yellow;

    private void OnDrawGizmos()
    {
        if (Visible)
        {
            Gizmos.color = lineColor;
            Gizmos.DrawWireSphere(transform.position, unit.GetRadius());
        }
    }
#endif
}

