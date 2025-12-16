using UnityEngine;

public class Breakable : MonoBehaviour
{
    public GameObject particlesPrefab;
    [SerializeField] int health = 1;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            health--;
            if (health <= 0)
            {
                PlayAndDestroy();
                DestroyBrick();
            }
        }
    }
    
    public void PlayAndDestroy()
    {
        SoundManager.Instance.Play("BreakBrick");
        GameObject ps = Instantiate(particlesPrefab, transform.position, particlesPrefab.transform.rotation);
        ParticleSystem particles = ps.GetComponent<ParticleSystem>();
        particles.Play();
        Destroy(ps, particles.main.startLifetime.constantMax);
    }

    public void DestroyBrick()
    {
        Destroy(gameObject);
    }
}
