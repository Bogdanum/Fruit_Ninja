using UnityEngine;

public class CameraBorders : MonoBehaviour
{
    private static float _border = 0;

    public static float Border
    {
        get
        {
            if (_border == 0)
            {
                var camera = Camera.main;
                _border = camera.aspect * camera.orthographicSize;
            }
            return _border;
        }
    }
}
