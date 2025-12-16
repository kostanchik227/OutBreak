using UnityEngine;
using System.Collections;

public class LaserManager : BaseManager
{
    protected IEnumerator Wave(float waveDuration = 5f, float interval = 0.13f, float laserSpeed = 6f, Side side = Side.Left)
    {
        CameraShake.Instance.ShakeForDuration(waveDuration, 0.03f);
        StartRepeating(() => {
            SpawnLaserBullet(Horizontal(
                y: Random.Range(-5.1f, 0.1f),
                side: side
            ), laserSpeed);
        }, interval, waveDuration);
        yield return new WaitForSeconds(waveDuration);
    }

    protected IEnumerator WaveTarget(float waveDuration = 5f, float interval = 0.15f, float laserSpeed = 9f, Side side = Side.Right)
    {
        CameraShake.Instance.ShakeForDuration(waveDuration, 0.03f);
        StartRepeating(() => {
            SpawnLaserBullet(Horizontal(
                y: PlayerPosition().y + Random.Range(-0.3f, 0.3f),
                side: side
            ), laserSpeed);
        }, interval, waveDuration);
        yield return new WaitForSeconds(waveDuration);
    }

    protected IEnumerator RandomStorm(float waveDuration = 10f, float interval = 0.3f, float laserSpeed = 6f)
    {
        CameraShake.Instance.ShakeForDuration(waveDuration, 0.03f);
        StartRepeating(() => {
            SpawnLaserBullet(AiminingAt(
                targetX: Random.Range(-7.1f, 7.1f), 
                targetY: Random.Range(-5.1f, 0.1f), 
                fromAngle: Random.Range(0f, 180f)
            ), laserSpeed);
        }, interval, waveDuration);
        yield return new WaitForSeconds(waveDuration);
    }

    protected IEnumerator RandomStormTarget(float waveDuration = 10f, float interval = 0.3f, float laserSpeed = 6f)
    {
        CameraShake.Instance.ShakeForDuration(waveDuration, 0.03f);
        StartRepeating(() => {
            Vector3 playerPos = PlayerPosition();
            SpawnParams spawnParams = AiminingAt(
                 targetPosition: playerPos,
                  fromAngle: Random.Range(-15f, 195f)
            );
            SpawnLaserBullet(spawnParams, laserSpeed);
            ControllerVisualLaser.Instance.CreateLaserTarget(
                startPos: spawnParams.position,
                targetPos: playerPos,
                laserSpeed: laserSpeed
            );
        }, interval, waveDuration);
        yield return new WaitForSeconds(waveDuration);
    }

    protected IEnumerator WarningWave(float waveDuration = 5f, float interval = 1.0f, float gap = 0.8f, Side side = Side.Left)
    {
        CameraShake.Instance.ShakeForDuration(waveDuration, 0.03f);

        StartRepeating(() => {
            float offset = Random.Range(0f, gap);
            float MAXY = 0.1f;
            float MINY = -5.1f;
            for (float curY = MINY + offset; curY < MAXY; curY += gap)
            {
                SpawnWarningLaserBullet(Horizontal(curY, side));
            }            
        }, interval, waveDuration);
        yield return new WaitForSeconds(waveDuration);
    }

    protected IEnumerator WarningWaveWithSpace(float waveDuration = 5f, float interval = 1.2f, Side side = Side.Left)
    {
        CameraShake.Instance.ShakeForDuration(waveDuration, 0.03f);

        StartRepeating(() => {
            float gap = 0.5f;
            float offset = 0.1f;
            float MAXY = 0.1f;
            float MINY = -4.9f;

            int cnt = (int)((MAXY - MINY) / gap);
            int safeNum = Random.Range(0, cnt);
            int i = 0;
            for (float curY = MINY + offset; curY < MAXY; curY += gap, i++)
            {
                if (i != safeNum)
                {
                    SpawnWarningLaserBullet(Horizontal(curY, side));
                }
            }
        }, interval, waveDuration);
        yield return new WaitForSeconds(waveDuration);
    }
}
