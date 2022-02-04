using UnityEngine;

[ExecuteInEditMode]
public class GameZoneVisualization : MonoBehaviour
{
    [SerializeField] private Transform upperLeftCorner;
    [SerializeField] private Transform lowerLeftCorner;
    [SerializeField] private Transform topRightCorner;
    [SerializeField] private Transform bottomRightCorner;
    [SerializeField] private Color linesColor;

    private void Update()
    {
        DrawLines();
    }

    private void DrawLines()
    {
        Debug.DrawLine(upperLeftCorner.position, topRightCorner.position, linesColor);
        Debug.DrawLine(topRightCorner.position, bottomRightCorner.position, linesColor);
        Debug.DrawLine(bottomRightCorner.position, lowerLeftCorner.position, linesColor);
        Debug.DrawLine(lowerLeftCorner.position, upperLeftCorner.position, linesColor);
    }
}
