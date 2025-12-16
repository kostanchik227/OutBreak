using UnityEngine;
using System.Collections;

public class Level2LaserScript : LaserManager
{
    private IEnumerator Phase1()
    {
        yield return RandomStorm(
                waveDuration: 15f,
                interval: 0.4f,
                laserSpeed: 6f);
    }

    private IEnumerator Phase2()
    {
        for(int i = 0; i < 5; i++) {
            yield return new WaitForSeconds(3f);
            yield return RandomStormTarget(
                waveDuration: 3f,
                interval: 0.3f,
                laserSpeed: 7f);
        }
    }

    private IEnumerator Phase3()
    {
        for (int i = 0; i < 5; i++) {
            yield return new WaitForSeconds(2f);
            yield return RandomStorm(
                waveDuration: 5f,
                interval: 0.3f,
                laserSpeed: 8f);
            yield return new WaitForSeconds(2f);
            yield return RandomStormTarget(
                waveDuration: 3f,
                interval: 0.2f,
                laserSpeed: 9f);
        }
    }

    protected override IEnumerator Script()
    {
        yield return new WaitForSeconds(5f);
        yield return Phase1(); // 15f
        yield return new WaitForSeconds(5f);
        yield return Phase2(); // 30f
        while (true) {
            yield return Phase3();  // 12f
        }
    }
}
