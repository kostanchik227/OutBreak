using UnityEngine;
using System.Collections;

public class Level4LaserScript : LaserManager
{
    private IEnumerator Phase1()
    {
        yield return WarningWave(
                waveDuration: 7.5f,
                interval: 1.5f,
                gap: 1f,
                side: Side.Left);
        yield return new WaitForSeconds(3f);
        yield return WarningWaveWithSpace(
                waveDuration: 7.5f,
                interval: 1.5f,
                side: Side.Right);
    }

    private IEnumerator Phase2()
    {
        yield return WarningWave(
                waveDuration: 7.5f,
                interval: 1f,
                gap: 1f,
                side: Side.Right);
        yield return new WaitForSeconds(3f);
        yield return WarningWaveWithSpace(
                waveDuration: 7.5f,
                interval: 1.5f,
                side: Side.Left);
    }

    private IEnumerator Phase3()
    {
        for (int i = 0; i < 10; i++) {
            yield return new WaitForSeconds(3f);
            StartCoroutine(WarningWave(
                waveDuration: 7.5f,
                interval: 1.5f,
                gap: 1f,
                side: Side.Left));
            StartCoroutine(WaveTarget(
                waveDuration: 7.5f,
                interval: 0.15f,
                laserSpeed: 9f,
                side: Side.Left));
            yield return new WaitForSeconds(10.5f);
            yield return WarningWaveWithSpace(
                    waveDuration: 7.5f,
                    interval: 1.5f,
                    side: Side.Left);
        }
    }

    protected override IEnumerator Script()
    {
        yield return new WaitForSeconds(5f);
        yield return Phase1(); // 18f
        yield return new WaitForSeconds(5f);
        yield return Phase2(); // 18f
        while (true) {
            yield return Phase3();  // 210f
        }
    }
}
