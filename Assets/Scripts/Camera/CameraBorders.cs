using System;
using UnityEngine;

public class CameraBorders : MonoBehaviour
{
    [SerializeField] private Camera camera;

    public static CameraBorders Instance;
    
    private float _border = 0;

    private void Awake()
    {
        Instance = this;
    }

    public float Border
    {
        get
        {
            if (_border == 0)
            {
                _border = camera.aspect * camera.orthographicSize;
            }
            return _border;
        }
    }
}
