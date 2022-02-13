using UnityEngine;

public class ParticleController : MonoBehaviour
{
    private ParticleSystem _particleSystem;

    public void Init(ParticleSystem particleSystem)
    {
        _particleSystem = particleSystem;
    }

    public void ChangeParticleColor(Color newColor)
    {
        var settings = _particleSystem.main;
        settings.startColor = newColor;
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
