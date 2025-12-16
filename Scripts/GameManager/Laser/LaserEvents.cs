using UnityEngine;

public class LaserEvents : MonoBehaviour
{
    private LaserPrefabs prefabs = null;
    private float LaserZPosition = -2.1f;

    public float XCameraBorder = 13f;
    public float YCameraBorder = 7f;

    protected virtual void Awake()
    {
        prefabs = GetComponent<LaserPrefabs>();
    }

    public struct SpawnParams
    {
        public Vector3 position;
        public float rotation;
    }

    protected Vector2 PlayerPosition()
    {
        return FollowMouse.Instance.GetPositionPlatform();
    }

    protected enum Side { Left, Right }

    protected SpawnParams Horizontal(float y, Side side = Side.Left)
    {
        float x = (side == Side.Left) ? -XCameraBorder : XCameraBorder;
        float rotation = (side == Side.Left) ? 0f : 180f;
        return new SpawnParams
        {
            position = new Vector3(x, y, LaserZPosition),
            rotation = rotation
        };
    }

    protected SpawnParams AiminingAt(float targetX, float targetY, float fromAngle)
    {
        Vector3 position = new Vector3(targetX, targetY, LaserZPosition);
        float angleRad = fromAngle * Mathf.Deg2Rad;
        Vector2 dir = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));

        float tRight = (dir.x > 0) ? (XCameraBorder - position.x) / dir.x : float.PositiveInfinity;
        float tLeft = (dir.x < 0) ? (-XCameraBorder - position.x) / dir.x : float.PositiveInfinity;
        float tTop = (dir.y > 0) ? (YCameraBorder - position.y) / dir.y : float.PositiveInfinity;
        float tBottom = (dir.y < 0) ? (-YCameraBorder - position.y) / dir.y : float.PositiveInfinity;

        float t = Mathf.Min(tRight, tLeft, tTop, tBottom);
        Vector3 spawnPosition = position + new Vector3(dir.x, dir.y, 0f) * t;

        return new SpawnParams
        {
            position = spawnPosition,
            rotation = fromAngle + 180f
        };
    }

    protected SpawnParams AiminingAt(Vector2 targetPosition, float fromAngle)
    {
        return AiminingAt(targetPosition.x, targetPosition.y, fromAngle);
    }

    protected void SpawnLaserBullet(SpawnParams spawnParams, float laserSpeed)
    {
        GameObject bullet = Instantiate(
            prefabs.LaserBullet,
            spawnParams.position,
            Quaternion.Euler(0f, 0f, spawnParams.rotation)
        );
        bullet.GetComponent<LaserMovement>().Speed = laserSpeed;
    }

    protected void SpawnWarningLaserBullet(SpawnParams spawnParams)
    {
        GameObject bullet = Instantiate(
            prefabs.WarningBullet,
            spawnParams.position,
            Quaternion.Euler(0f, 0f, spawnParams.rotation)
        );
    }
}
