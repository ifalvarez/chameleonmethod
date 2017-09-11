using System.Collections;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public BirdDectection detectionArea;
    public float flySpeed = 0.0f;
    public float maxDistanceToTurn = 0.0f;
    Vector3 startPosition;

    public enum TurnDirection
    {
        left,
        right
    }
    TurnDirection turn = TurnDirection.left;

    private void Awake()
    {
        detectionArea.OnPlayerDetect += OnPlayerDetected;
        startPosition = transform.position;
    }

    private void OnPlayerDetected(Transform detectedPosition)
    {
        StopAllCoroutines();
        attackPlayer = AttackPlayer(detectedPosition.position);
    }

    IEnumerator flyAround;
    IEnumerator FlyAround()
    {
        while(true)
        {
            transform.position += transform.forward * flySpeed * Time.deltaTime;
            if (Vector3.Distance(startPosition, transform.position) >= maxDistanceToTurn)
            {

            }
            yield return null;
        }
    }

    IEnumerator attackPlayer;
    IEnumerator AttackPlayer(Vector3 target)
    {
        while(true)
        {
            yield return null;
        }
    }
}