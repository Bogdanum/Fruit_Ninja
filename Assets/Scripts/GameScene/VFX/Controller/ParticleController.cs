using UnityEngine;

public class ParticleController : MonoBehaviour
{
    private ParticleSystem _particleSystem;

    public void Init(ParticleSystem particleSystem)
    {
        _particleSystem = particleSystem;
    }

    public void ChangeParticleProperties(Color newColor)
    {
        var settings = _particleSystem.main;
        settings.startColor = newColor;
    }

    public void ChangeParticleProperties(Color newColor, float newDuration)
    {
        var settings = _particleSystem.main;
        settings.startColor = newColor;
        settings.duration = newDuration;
    }

    public void Play()
    {
        _particleSystem.Play();
    }

    public void Pause()
    {
        _particleSystem.Pause();
    }

    public bool IsPlaying()
    {
        return _particleSystem.isPlaying;
    }
}
