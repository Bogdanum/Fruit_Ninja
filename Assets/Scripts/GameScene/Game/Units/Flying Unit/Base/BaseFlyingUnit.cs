using System.Collections;
using UnityEngine;

public class BaseFlyingUnit : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private PhysicsBody physicsBody;
    [SerializeField] private ParticleController particleController;

    private Vector2 _bladePosition;
    private Effect _unitEffect;
    private FlyingUnitSettings _settings;

    public bool Active { get; private set; }
    public bool FreeFall { get; set; }
    public float Radius => _settings.physicsParameters.radius;

    public void Init(FlyingUnitSettings settings)
    {
        _settings = settings;
        _unitEffect = settings.unitEffect;
        spriteRenderer.sprite = settings.Sprite;
        spriteRenderer.sortingOrder = transform.GetInstanceID();
        spriteRenderer.sortingLayerName = settings.sortingLayerName;
        FreeFall = false;
        InputEvents.MousePosition.AddListener(UpdateBladePosition);
    }

    private void UpdateBladePosition(Vector3 bladePosition)
    {
        _bladePosition = bladePosition;
    }

    public void Launch(Vector2 direction, Vector3 startPosition)
    {
        Active = true;
        transform.position = startPosition;
        transform.localScale = _settings.physicsParameters.scale;
        gameObject.SetActive(true);
        physicsBody.Init(_settings.physicsParameters.mass, direction.y, direction.x, RandomRotationSpeed());
    }
    
    private float RandomRotationSpeed()
    {
        var physics = _settings.physicsParameters;
        float rotationSpeed = Random.Range(physics.minRotationSpeed, physics.maxRotationSpeed);
        int direction = Random.Range(0, 3);
        return direction > 1 ? rotationSpeed : -rotationSpeed;
    }

    private void Update()
    {
        if (!Active) return;

        if (OutOfBounds())
        {
            Active = false;
            physicsBody.Deactivate();
            _settings.unitEffect.OutOfBounds(this);
        }
        CheckCut();
    }
    
    private bool OutOfBounds()
    {
        var gameZone = GameZone.Instance;
        return transform.position.y < gameZone.BottomLine * gameZone.DeathlineOffset;
    }
    
    private void CheckCut()
    {
        if (FreeFall) return;
        
        if (Blade.IsSwipeCut)
        {
            float distance = Vector2.Distance(transform.position, _bladePosition);

            if (distance < _settings.physicsParameters.radius)
            {
                _unitEffect.Perform(_settings, particleController, this, physicsBody);
                physicsBody.Deactivate();
                _unitEffect.ReturnToPool(this);
            }
        }
    }

    public void EndEffectWithDelay(float delay) => StartCoroutine(EndEffect(delay));

    private IEnumerator EndEffect(float delay)
    {
        yield return new WaitForSeconds(delay);
        _unitEffect.End(physicsBody);
    }
}