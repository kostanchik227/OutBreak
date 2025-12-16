using UnityEngine;

public class TriggerZoneHomingLaser : MonoBehaviour
{
    private LaserHoming parent;

    private void Awake()
    {
        parent = gameObject.transform.GetComponentInParent<LaserHoming>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) {
            parent.Explode();
        }
    }
}
