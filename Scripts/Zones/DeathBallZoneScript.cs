using UnityEngine;

public class KillScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            Debug.Log("Game over (Ball in Death Zone)");
            Destroy(other.gameObject);
            GameManager.Instance.EndGame(false);
        }
    }
}
