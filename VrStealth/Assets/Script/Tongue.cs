using UnityEngine;
using System.Collections;

public class Tongue : MonoBehaviour
{
    public Transform body;
    public Transform tongueTip;
    public float tongueStretchSpeed = 0.0f;
    public float maxTongueDistance = 0.0f;
    public float moveToTargetSpeed = 0.0f;
    float initialDistance = 0.0f;
    Vector3 targetStartPosition;
    bool stretching = false;

    private void Awake()
    {
        TongueTip.OnTongueHit += StartMoveToTarget;
        initialDistance = Vector3.Distance(transform.position, tongueTip.position);
        targetStartPosition = tongueTip.localPosition;
    }

    void StartMoveToTarget(Vector3 hitPoint)
    {
        if(moveTorwardsTarget != null)
        {
            StopCoroutine(moveTorwardsTarget);
        }
        if (fireTongue != null)
        {
            StopCoroutine(fireTongue);
        }
        moveTorwardsTarget = moveToTarget(hitPoint);
        StartCoroutine(moveTorwardsTarget);
    }

    IEnumerator moveTorwardsTarget;
    IEnumerator moveToTarget(Vector3 hitPoint)
    {
        while(true)
        {
            float lastDistance = Vector3.Distance(transform.position, tongueTip.position);
            Debug.Log(Vector3.Distance(body.position, hitPoint));
            body.position = Vector3.MoveTowards(body.position, hitPoint, moveToTargetSpeed);
            transform.LookAt(tongueTip);
            //if(Vector3.Distance(transform.position, hitPoint) <= 1.0f)
            if(lastDistance < Vector3.Distance(transform.position, hitPoint) || Vector3.Distance(body.position, hitPoint) <= 1.0f)
            {
                body.position = hitPoint;
                tongueTip.transform.SetParent(Camera.main.transform);
                tongueTip.localPosition = targetStartPosition;
                tongueTip.localRotation = Quaternion.Euler(Vector3.zero);
                transform.localScale = Vector3.one;
                transform.LookAt(tongueTip.position);
                break;
            }
            yield return null;
        }
    }

    IEnumerator fireTongue;
    IEnumerator TungueFire()
    {
        stretching = true;
        tongueTip.transform.parent = null;
        while(true)
        {
            if (stretching == true)
            {
                tongueTip.position += tongueTip.forward * tongueStretchSpeed * Time.deltaTime;
                transform.LookAt(tongueTip.position);
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, Vector3.Distance(transform.position, tongueTip.position) * 1.0f / initialDistance);
                if(Vector3.Distance(transform.position, tongueTip.position) >= maxTongueDistance)
                {
                    stretching = false;
                }
            }
            else
            {
                float lastDistance = Vector3.Distance(transform.position, tongueTip.position);
                tongueTip.transform.LookAt(transform);
                tongueTip.position += tongueTip.forward * tongueStretchSpeed * Time.deltaTime;
                transform.LookAt(tongueTip.position);
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, Vector3.Distance(transform.position, tongueTip.position) * 1.0f / initialDistance);
                if(lastDistance < Vector3.Distance(transform.position, tongueTip.position))
                {
                    tongueTip.transform.SetParent(Camera.main.transform);
                    tongueTip.localPosition = targetStartPosition;
                    tongueTip.localRotation = Quaternion.Euler(Vector3.zero);
                    transform.localScale = Vector3.one;
                    transform.LookAt(tongueTip.position);
                    break;
                }
            }
            yield return null;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(fireTongue != null)
            {
                StopCoroutine(fireTongue);
            }
            fireTongue = TungueFire();
            StartCoroutine(fireTongue);
        }
    }
}
