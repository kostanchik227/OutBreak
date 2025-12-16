using UnityEngine;
using System.Collections;

public class Level1LaserScript : LaserManager 
{
    private IEnumerator Phase1()
    {
        yield return RandomStorm(
                waveDuration: 15f,
                interval: 0.5f,
                laserSpeed: 4.5f);
    }

    private IEnumerator Phase2()
    {
        yield return RandomStorm(
                waveDuration: 30f,
                interval: 0.45f,
                laserSpeed: 5.5f);
    }

    private IEnumerator Phase3()
    {
        yield return RandomStorm(
                waveDuration: 20f,
                interval: 0.4f,
                laserSpeed: 6f);
    }

    protected override IEnumerator Script()
    {
        yield return new WaitForSeconds(5f);
        yield return Phase1(); // 15f
        yield return new WaitForSeconds(5f);
        yield return Phase2(); // 30f
        while (true) {
            yield return new WaitForSeconds(3f);
            yield return Phase3();  // 20f
        }
    }
}
