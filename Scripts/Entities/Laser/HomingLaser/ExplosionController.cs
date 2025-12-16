using UnityEngine;

public class ExplosionController : MonoBehaviour {
    public float lifeTime = 1.5f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }
}
