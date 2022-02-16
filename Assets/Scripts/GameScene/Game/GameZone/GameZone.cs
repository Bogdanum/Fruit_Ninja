using UnityEngine;

[ExecuteInEditMode]
public class GameZone : Singleton<GameZone>
{
    [SerializeField] private Transform upperLeftCorner;
    [SerializeField] private Transform lowerLeftCorner;
    [SerializeField] private Transform topRightCorner;
    [SerializeField] private Transform bottomRightCorner;
    [Space(20)]
    [SerializeField] private GameZoneSettings settings;
    [SerializeField] private Camera camera;

    protected override void Awake()
    {
        base.Awake();
        Init();
    }

    private void Init()
    {
        upperLeftCorner.position = new Vector3(settings.leftWall, settings.topWall, 0);
        lowerLeftCorner.position = new Vector3(settings.leftWall, settings.bottomWall, 0);
        topRightCorner.position = new Vector3(settings.rightWall, settings.topWall, 0);
        bottomRightCorner.position = new Vector3(settings.rightWall, settings.bottomWall, 0);
        ZoneCorrection();
    }

    public void ZoneCorrection()
    {
        upperLeftCorner.position = GetAdjastedPosition(upperLeftCorner.position, true);
        lowerLeftCorner.position = GetAdjastedPosition(lowerLeftCorner.position, true);
        topRightCorner.position = GetAdjastedPosition(topRightCorner.position, false);
        bottomRightCorner.position = GetAdjastedPosition(bottomRightCorner.position, false);
    }

    private Vector3 GetAdjastedPosition(Vector3 oldPos, bool left)
    {
        float absOldPosX = Mathf.Abs(oldPos.x);
        float oldAspectRatio = absOldPosX - camera.orthographicSize;
        float absNewX = (absOldPosX - oldAspectRatio) * camera.aspect;
        float newX = left ? -absNewX : absNewX;
        return new Vector3(newX, oldPos.y, 0);
    }

    public Vector3 UpperLeftCorner => upperLeftCorner.position;
    public Vector3 LowerLeftCorner => lowerLeftCorner.position;
    public Vector3 TopRightCorner => topRightCorner.position;
    public Vector3 BottomRightCorner => bottomRightCorner.position;


    public float CameraOrthographicSize => camera.orthographicSize;
    public float CameraAspect => camera.aspect;
    public float BottomLine => BottomRightCorner.y;
    public float TopLine => TopRightCorner.y;

    public float RightLine => BottomRightCorner.x;
    public float LeftLine => LowerLeftCorner.x;
}
