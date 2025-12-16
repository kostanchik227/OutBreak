using UnityEngine;

public class ControllerVisualLaser : MonoBehaviour
{
    public static ControllerVisualLaser Instance;

    public GameObject LaserTarget;

    private void Awake()
    {
        Instance = this;
    }

    public void CreateLaserTarget(Vector3 startPos, Vector3 targetPos, float laserSpeed)
    {
        if (LaserTarget == null) {
            Debug.LogWarning("LaserTarget prefab is not assigned in Inspector!");
            return;
        }
        float timeLife = CalculateLifeTime(startPos, targetPos, laserSpeed);

        GameObject target = Instantiate(LaserTarget, targetPos, Quaternion.identity);
        target.GetComponent<TargetVisual>().delay = timeLife;
        Vector3 pos = target.transform.position;
        pos.z = -1f;
        target.transform.position = pos;
        target.GetComponent<TargetVisual>().StartDestruct();
    }

    private float CalculateLifeTime(Vector3 startPos, Vector3 targetPos, float laserSpeed)
    {
        float distance = Vector2.Distance(startPos, targetPos);

        float timeToHit = distance / laserSpeed;
        return timeToHit;
    }
}
