using System.Collections;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public Animator birdAnim;
    public BirdDectection detectionArea;
    public float flySpeed = 0.0f;
    public float maxDistanceToTurn = 0.0f;
    public float rotationSpeed = 0.0f;
    public float minReturnAngle = 0.0f;
    public float attackUpOffSet = 0.0f;
    public float attackDistanceOffSet = 0.0f;
    Vector3 startPosition;
    public bool left = false;

    public enum BirdState
    {
        move,
        turning
    }
    public BirdState state = BirdState.move;

    private void Awake()
    {
        detectionArea.OnPlayerDetect += OnPlayerDetected;
        startPosition = transform.position;
    }

    private void Start()
    {
        flyAround = FlyAround();
        StartCoroutine(flyAround);
    }

    private void OnPlayerDetected(Transform detectedPosition)
    {
        StopAllCoroutines();
        attackPlayer = AttackPlayer(detectedPosition);
        StartCoroutine(attackPlayer);
        birdAnim.SetTrigger("Attack");
    }

    IEnumerator flyAround;
    IEnumerator FlyAround()
    {
        while(true)
        {
            if (state == BirdState.move)
            {
                transform.position += transform.forward * flySpeed * Time.deltaTime;
                if (Vector3.Distance(startPosition, transform.position) >= maxDistanceToTurn && Vector3.Angle(new Vector3(startPosition.x, 0.0f, startPosition.z) - new Vector3(transform.position.x, 0.0f, transform.position.z), transform.forward) >= minReturnAngle)
                {
                    state = BirdState.turning;
                }
            }
            else if(state == BirdState.turning)
            {
                transform.position += transform.forward * flySpeed * Time.deltaTime;
                if(left == false)
                {
                    transform.localRotation *= Quaternion.Euler(0.0f, rotationSpeed * Time.deltaTime, 0.0f);
                }
                else
                {
                    transform.localRotation *= Quaternion.Euler(0.0f, -rotationSpeed * Time.deltaTime, 0.0f);
                }

                if(Vector3.Angle(new Vector3(startPosition.x, 0.0f, startPosition.z) - new Vector3(transform.position.x, 0.0f, transform.position.z), transform.forward) <= minReturnAngle)
                {
                    left = left == false ? true : false;
                    state = BirdState.move;
                    transform.LookAt(startPosition);
                }
            }
            yield return null;
        }
    }

    IEnumerator attackPlayer;
    IEnumerator AttackPlayer(Transform target)
    {
        while(true)
        {
            transform.LookAt(target.position + (Vector3.up * attackUpOffSet));
            transform.position += transform.forward * flySpeed * Time.deltaTime;
            if(Vector3.Distance(transform.position, target.position + (Vector3.up * attackUpOffSet)) <= attackDistanceOffSet)
            {
                Debug.Log("Game Over");
                break;
            }
            yield return null;
        }
    }
}