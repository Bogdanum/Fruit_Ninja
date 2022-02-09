using UnityEngine;

public class Blade : MonoBehaviour
{
    [SerializeField] private GameObject trail;
    [SerializeField] private BladeSettings bladeSettings;
    
    private bool isCutting = false;
    private bool isGameOver = false;
    private Vector2 _prevBladePosition;
    private Vector2 _currentMousePosition;

    public static bool IsSwipeCut { get; private set; }
    private void Awake()
    {
        GameplayEvents.GameOver.AddListener(BlockBlade);
        InputEvents.MouseClickOrTouch.AddListener(StartCutting);
        InputEvents.MouseUp.AddListener(StopCutting);
        InputEvents.MousePosition.AddListener(SetMousePosition);
    }

    private void BlockBlade()
    {
        isGameOver = true;
    }

    private void SetMousePosition(Vector3 position)
    {
        _currentMousePosition = position;
    }
    
    private void Update()
    {
        if (isGameOver)
        {
            return;
        }
        
        if (isCutting)
        {
            UpdateCut();
        }
        transform.position = _currentMousePosition;
    }

    private void StartCutting()
    {
        isCutting = true;
        trail.SetActive(true);
        IsSwipeCut = false;
    }

    private void StopCutting()
    {
        isCutting = false;
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
