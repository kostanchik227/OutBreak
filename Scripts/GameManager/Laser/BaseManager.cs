using UnityEngine;
using System.Collections;

public class BaseManager : LaserEvents
{
    public static BaseManager Instance { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        Instance = this;
    }

    protected IEnumerator RepeatingCoroutine(System.Action action, float interval, float duration, float delay = 0f)
    {
        yield return new WaitForSeconds(delay);
        for (float elapsed = 0f; elapsed < duration; elapsed += interval)
        {
            action?.Invoke();
            yield return new WaitForSeconds(interval);
        }
    }

    protected void StartRepeating(System.Action action, float interval, float duration, float delay = 0f)
    {
        StartCoroutine(RepeatingCoroutine(action, interval, duration, delay));
    }

    protected virtual IEnumerator Script() { yield break; }

    public void RunScript()
    {
        StartCoroutine(Script());
    }
}
