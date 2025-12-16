using UnityEngine;
using System.Collections;

public class Level3LaserScript : LaserManager
{
    private IEnumerator Phase1()
    {
        yield return Wave(
                waveDuration: 7f,
                interval: 0.2f,
                laserSpeed: 6f,
                side: Side.Left);
        yield return new WaitForSeconds(3f);
        yield return RandomStormTarget(
                waveDuration: 5f,
                interval: 0.25f,
                laserSpeed: 7f);
    }

    private IEnumerator Phase2()
    {
        for (int i = 0; i < 4; i++) {
            yield return new WaitForSeconds(3f);
            yield return Wave(
                waveDuration: 5f,
                interval: 0.2f,
                laserSpeed: 6f,
                side: (i%2==0) ? Side.Left : Side.Right);
        }
    }

    private IEnumerator Phase3()
    {
        for (int i = 0; i < 10; i++) {
            yield return new WaitForSeconds(3f);
            yield return Wave(
                waveDuration: 5f,
                interval: 0.18f,
                laserSpeed: 7f,
                side: (i % 2 == 0) ? Side.Left : Side.Right);
            yield return new WaitForSeconds(3f);
            yield return RandomStormTarget(
                waveDuration: 5f,
                interval: 0.23f,
                laserSpeed: 7.5f);
        }
    }

    protected override IEnumerator Script()
    {
        yield return new WaitForSeconds(5f);
        yield return Phase1(); // 15f
        yield return new WaitForSeconds(5f);
        yield return Phase2(); // 32f
        while (true) {
            yield return Phase3();  // 160f
        }
    }
}
