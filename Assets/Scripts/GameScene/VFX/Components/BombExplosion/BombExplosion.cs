using System.Collections;
using UnityEngine;

public class BombExplosion : MonoBehaviour
{
    [SerializeField, Range(0.5f, 2)] private float timeToReturnToPool;

    public void Init(Vector3 position)
    {
        gameObject.SetActive(true);
        transform.position = position;
        StartCoroutine(LifeTime());
    }

    private IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(timeToReturnToPool);
        ExplosionsPool.Instance.ReturnToPool(this);
    }
}
