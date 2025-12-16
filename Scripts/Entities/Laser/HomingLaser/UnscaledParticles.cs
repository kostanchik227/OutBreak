using UnityEngine;

public class UnscaledParticles : MonoBehaviour {
    private ParticleSystem ps;

    void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        ps.Simulate(Time.unscaledDeltaTime, true, false, false);
    }
}
