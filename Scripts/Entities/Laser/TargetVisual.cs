using UnityEngine;
using System.Collections;

public class TargetVisual : MonoBehaviour {
    [Tooltip("Seconds until this GameObject is destroyed")]
    public float delay = 2f;

    private Coroutine destroyRoutine;


    public void StartDestruct()
    {
        if (destroyRoutine != null)
            StopCoroutine(destroyRoutine);

        destroyRoutine = StartCoroutine(DoDestroyAfterDelay(delay));
    }

    public void CancelDestruct()
    {
        if (destroyRoutine != null) {
            StopCoroutine(destroyRoutine);
            destroyRoutine = null;
        }
    }

    public void ResetDestruct()
    {
        CancelDestruct();
        StartDestruct();
    }

    private IEnumerator DoDestroyAfterDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
