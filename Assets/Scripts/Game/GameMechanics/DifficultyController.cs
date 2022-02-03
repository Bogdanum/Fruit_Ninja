using UnityEngine;

public class DifficultyController : MonoBehaviour
{
    [SerializeField, Range(1, 100)] 
    private float timeToIncreaseDifficulty;

    private float _timer = 0;
    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > timeToIncreaseDifficulty)
        {
            _timer = 0;
            GameplayEvents.SendIncreasingComplexityEvent();
        }
    }
}
