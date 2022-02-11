using UnityEngine;

public class Heart : MonoBehaviour
{
    [SerializeField] private TweenScaler scaler;

    public void Show(float duration)
    {
        gameObject.SetActive(true);
        transform.localScale = Vector3.zero;
        SmoothAppearance(duration);
    }

    private void SmoothAppearance(float duration)
    {
        scaler.DoScale(Vector3.one, duration);
    }

    public void Hide(float duration)
    {
        scaler.DoScale(Vector3.zero, duration, () => gameObject.SetActive(false));
    }
}
