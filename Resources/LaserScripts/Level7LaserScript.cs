using UnityEngine;
using System.Collections;

public class Level7LaserScript : LaserManager
{
    private IEnumerator Phase1()
    {
        float startSpeed = 5f, endSpeed = 7f;
        float startInterval = 0.3f, endInterval = 0.2f;
        float time = 15f;

        int steps = 10;
        for (int i = 0; i < steps; i++) {
            float t = (float)i / (steps - 1);

            float currentSpeed = Mathf.Lerp(startSpeed, endSpeed, t);
            float currentInterval = Mathf.Lerp(startInterval, endInterval, t);

            yield return RandomStorm(
                waveDuration: (time / steps),
                interval: currentInterval,
                laserSpeed: currentSpeed);
        }
    }

    private IEnumerator Phase2()
    {
        float startSpeed = 8f, endSpeed = 15f;
        float startInterval = 0.2f, endInterval = 0.1f;
        float time = 35f;

        int steps = 10;
        for (int i = 0; i < steps; i++) {
            float t = (float)i / (steps - 1);

            float currentSpeed = Mathf.Lerp(startSpeed, endSpeed, t);
            float currentInterval = Mathf.Lerp(startInterval, endInterval, t);

            yield return RandomStorm(
                waveDuration: (time / steps),
                interval: currentInterval,
                laserSpeed: currentSpeed);
        }
    }

    private IEnumerator Phase3()
    {
        float startSpeed = 15f, endSpeed = 50f;
        float startInterval = 0.15f, endInterval = 0.05f;
        float time = 100f;

        int steps = 10;
        for (int i = 0; i < steps; i++) {
            float t = (float)i / (steps - 1);

            float currentSpeed = Mathf.Lerp(startSpeed, endSpeed, t);
            float currentInterval = Mathf.Lerp(startInterval, endInterval, t);

            yield return RandomStorm(
                waveDuration: (time / steps),
                interval: currentInterval,
                laserSpeed: currentSpeed);
        }
    }

    protected override IEnumerator Script()
    {
        yield return new WaitForSeconds(5f);
        yield return Phase1(); // 15f
        yield return new WaitForSeconds(5f);
        yield return Phase2(); // 35f
        while (true) {
            yield return new WaitForSeconds(5f);
            yield return Phase3();  // 100f
        }
    }
}
