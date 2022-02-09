using System.Collections;
using UnityEngine;

public class SplashOnWall : MonoBehaviour
{
    [SerializeField] private SplashOnWallSettings settings;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;

    public void Init(Sprite splashSprite)
    {
        spriteRenderer.sprite = splashSprite;
        spriteRenderer.sortingOrder = transform.GetInstanceID();
        transform.localScale = settings.scale;
        gameObject.SetActive(true);
        transform.Rotate(0,0, Random.Range(-45, 45));
        StartCoroutine(SplashLife());
    }

    private IEnumerator SplashLife()
    {
        yield return new WaitForSeconds(settings.lifeTime);

        animator.SetBool(settings.animationTriggerName, true);
        yield return new WaitForSeconds(settings.timeForAnimation);
        
        SplashPool.Instance.ReturnToPool(this);
    }
}
