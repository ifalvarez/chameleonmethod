using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BehaviourTargetAim : MonoBehaviour {

    [SerializeField] private float maxDistanceAction;
    [SerializeField] private float timeToShowTarget;
    [SerializeField] private float timeToHideTarget;
    [SerializeField] private Image targetAim;
    [SerializeField] private GameObject objectPoint;

    private float timerToHide;
    private float timerToShow;
    private float currentOpacity;
    private float lastOpacity;

    private Ray distanceRay;
    private RaycastHit hitInfo;
    public bool isInteracting;

    private void Start()
    {
        showTarget = ShowTarget();
        hideTarget = HideTarget();
        isInteracting = false;
        currentOpacity = targetAim.color.a;
    }

    private void FixedUpdate()
    {       
        if (Physics.Raycast(objectPoint.transform.position, objectPoint.transform.forward.normalized * maxDistanceAction, out hitInfo))
        {
            if (!isInteracting)
            {
                timerToShow = 0;
                StopAllCoroutines();
                showTarget = ShowTarget();
                StartCoroutine(showTarget);
                isInteracting = true;
            }                    
        }
        else
        {
            if (isInteracting)
            {
                timerToHide = 0;
                StopAllCoroutines();
                hideTarget = HideTarget();
                StartCoroutine(hideTarget);
                isInteracting = false;
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawRay(objectPoint.transform.position, objectPoint.transform.forward.normalized * maxDistanceAction);
    }

    IEnumerator showTarget;
    IEnumerator ShowTarget()
    {
        Color newColor = targetAim.color;
        lastOpacity = currentOpacity;

        while (currentOpacity < 1)
        {            
            timerToShow += Time.deltaTime;
            currentOpacity = Mathf.Lerp(lastOpacity, 1, timerToShow / timeToShowTarget);
            print(currentOpacity);
            newColor.a = currentOpacity;
            targetAim.color = newColor;
            yield return null;
        }
    }

    IEnumerator hideTarget;
    IEnumerator HideTarget()
    {
        Color newColor = targetAim.color;
        lastOpacity = currentOpacity;
        
        while (currentOpacity > 0.2)
        {
            timerToHide += Time.deltaTime;
            currentOpacity = Mathf.Lerp(lastOpacity, 0, timerToHide / timeToHideTarget);
            newColor.a = currentOpacity;
            targetAim.color = newColor;
            yield return null;
        }
    }
}
