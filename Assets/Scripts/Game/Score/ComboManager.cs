using UnityEngine;

public class ComboManager : Singleton<ComboManager>
{
    [SerializeField] private ComboSettings settings;
    
    private int _multiplier = 1;
    private float _timer = 0;
    private void Update()
    {
        _timer += Time.deltaTime;
    }

    public int GetMultiplier()
    {
        if (_timer <= settings.minDestroyInterval)
        {
            if (_multiplier < settings.maxMultiplier)
            {
                _timer = 0;
                _multiplier++;
            }
        }
        else
        {
            _multiplier = 1;
            _timer = 0;
        }
        return _multiplier;
    }
}
