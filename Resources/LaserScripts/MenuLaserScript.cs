using UnityEngine;
using System.Collections;

public class MenuLaserScript : LaserManager {
    private IEnumerator Phase1()
    {
        yield return RandomStorm(1f, 0.03f, 35f);
    }

    protected override IEnumerator Script()
    {
        yield return new WaitForSeconds(5f);
        while (true)
        {
            yield return Phase1();  // 1f
            yield return new WaitForSeconds(4f);
        }
    }
}
