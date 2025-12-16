using UnityEngine;
using System.Collections;

public class Level9LaserScript : LaserManager
{
    private IEnumerator Phase1()
    {
        yield return Wave(5f);
    }

    protected override IEnumerator Script()
    {
        yield return new WaitForSeconds(5f);
        while (true)
        {
            yield return Phase1();  // 5f
        }
    }
}
