using UnityEngine;

public class DifficultyController : MonoBehaviour
{
    [SerializeField] private DifficultySettings settings;

    private float _timer = 0;

    private void Awake()
    {
        GameplayEvents.Restart.AddListener(RefreshTimer);
    }

    private void RefreshTimer() => _timer = 0;

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > settings.timeToIncreaseDifficulty)
        {
            _timer = 0;
            GameplayEvents.SendIncreasingComplexityEvent();
        }
    }
}
