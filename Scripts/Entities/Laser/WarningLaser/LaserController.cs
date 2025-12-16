using UnityEngine;
using System.Collections;
using System.Net;

public class LaserController : MonoBehaviour {
    [Header("References")]
    public LaserWarning warningLine;
    public LaserBeamMovement beam;

    [Header("Settings")]
    public float WarningLength = 20f;
    public float delayBetweenStages = 0.3f;

    private Vector2 startPoint;
    private Vector2 endPoint;

    void Start()
    {
        warningLine.Initialize();
        beam.Initialize();

        warningLine.gameObject.SetActive(false);
        beam.gameObject.SetActive(false);

        StartCoroutine(LaserSequence());
    }

    private IEnumerator LaserSequence()
    {
        CalculatePoints();
        
        warningLine.gameObject.SetActive(true);
        warningLine.PlayAnimation(startPoint, endPoint);

        yield return new WaitForSeconds(warningLine.GetTotalDuration() + delayBetweenStages);

        beam.gameObject.SetActive(true);
        beam.RunLaser();
    }

    private void Update()
    {
        if(beam == null) {
            Destroy(gameObject);
        }
    }

    private void CalculatePoints()
    {
        startPoint = transform.position;

        Vector2 direction = transform.right;
        endPoint = startPoint + direction * WarningLength;
    }
}