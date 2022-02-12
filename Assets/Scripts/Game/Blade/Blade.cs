using UnityEngine;

public class Blade : MonoBehaviour
{
    [SerializeField] private GameObject trailPrefab;
    [SerializeField] private BladeSettings bladeSettings;

    private GameObject currentTrail;
    private bool isCutting = false;
    private bool blocked = false;
    private Vector2 _prevBladePosition;
    private Vector2 _currentMousePosition;

    public static bool IsSwipeCut { get; private set; }
    private void Awake()
    {
        GameplayEvents.GameOver.AddListener(BlockBlade);
        GameplayEvents.Restart.AddListener(ActivateBlade);
        InputEvents.MouseClickOrTouch.AddListener(StartCutting);
        InputEvents.MouseUp.AddListener(StopCutting);
        InputEvents.MousePosition.AddListener(SetMousePosition);
    }

    private void BlockBlade() => blocked = true;

    private void ActivateBlade() => blocked = false;

    private void SetMousePosition(Vector3 position)
    {
        _currentMousePosition = position;
    }
    
    private void Update()
    {
        if (blocked)
        {
            return;
        }
        
        if (isCutting)
        {
            UpdateCut();
        }
    }

    private void StartCutting()
    {
        isCutting = true;
        _prevBladePosition = _currentMousePosition;
        IsSwipeCut = false;
        currentTrail = Instantiate(trailPrefab, transform);
        currentTrail.SetActive(false);
    }

    private void StopCutting()
    {
        isCutting = false;
        IsSwipeCut = false;
        currentTrail.transform.SetParent(null);
        Destroy(currentTrail, 0.3f);
    }
    
    private void UpdateCut()
    {
        currentTrail.SetActive(true);
        transform.position = _currentMousePosition;
        float velocity = (_currentMousePosition - _prevBladePosition).magnitude * Time.deltaTime;
        IsSwipeCut = velocity > bladeSettings.minCuttingVelocity ? true : false;
        _prevBladePosition = _currentMousePosition;
    }
}
