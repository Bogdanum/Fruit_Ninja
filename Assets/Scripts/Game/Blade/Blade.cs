using UnityEngine;

public class Blade : MonoBehaviour
{
    [SerializeField] private GameObject trail;
    [SerializeField] private BladeSettings bladeSettings;
    
    private bool IsCutting = false;
    private Vector2 _prevBladePosition;
    private Vector2 _currentMousePosition;

    public static bool IsSwipeCut { get; private set; }
    private void Awake()
    {
        InputEvents.MouseClickOrTouch.AddListener(StartCutting);
        InputEvents.MouseUp.AddListener(StopCutting);
        InputEvents.MousePosition.AddListener(SetMousePosition);
    }

    private void SetMousePosition(Vector3 position)
    {
        _currentMousePosition = position;
    }
    
    private void Update()
    {
        if (IsCutting)
        {
            UpdateCut();
        }
        transform.position = _currentMousePosition;
    }

    private void StartCutting()
    {
        IsCutting = true;
        trail.SetActive(true);
        IsSwipeCut = false;
    }

    private void StopCutting()
    {
        IsCutting = false;
        trail.SetActive(false);
        IsSwipeCut = false;
    }
    
    private void UpdateCut()
    {
        float velocity = (_currentMousePosition - _prevBladePosition).magnitude * Time.deltaTime;

        IsSwipeCut = velocity > bladeSettings.minCuttingVelocity ? true : false;
        _prevBladePosition = _currentMousePosition;
    }
}
