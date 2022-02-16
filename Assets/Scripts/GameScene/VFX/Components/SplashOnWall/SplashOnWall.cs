using System.Collections;
using UnityEngine;
using DG.Tweening;

public class SplashOnWall : MonoBehaviour
{
    [SerializeField] private SplashOnWallSettings settings;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public void Init(Color splashColor, Vector3 position)
    {
        spriteRenderer.color = splashColor;
        spriteRenderer.sortingOrder = transform.GetInstanceID();
        transform.position = position;
        transform.localScale = settings.scale;
        gameObject.SetActive(true);
        transform.Rotate(0,0, Random.Range(-45, 45));
        StartCoroutine(SplashLife());
    }

    private IEnumerator SplashLife()
    {
        yield return new WaitForSeconds(settings.lifeTime);

        var finishColor = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0);
        var tween = spriteRenderer.DOColor(finishColor, settings.timeForAnimation);
        tween.OnComplete(() =>
        {
            tween.Kill();
            SplashPool.Instance.ReturnToPool(this);
        });
    }
}
