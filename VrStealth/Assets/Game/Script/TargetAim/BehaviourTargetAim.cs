using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BehaviourTargetAim : MonoBehaviour {

    [SerializeField] private float maxDistanceAction;
    [SerializeField] private float timeToShowTarget;
    [SerializeField] private float timeToHideTarget;

    private float timerToHide;
    private float timerToShow;
    private float currentOpacity;
    private float lastOpacity;

    private Image targetAim;
    private Ray distanceRay;
    private RaycastHit hitInfo;
    public bool isInteracting;

    void Awake()
    {
        //SceneManager.sceneLoaded += AssignRayCastPosition;
    }

    /*private void AssignRayCastPosition(Scene arg0, LoadSceneMode arg1)
    {
            rayCastStartPoint = Tongue.tongueTransform.gameObject;        
    }*/

    private void Start()
    {
        targetAim = GetComponent<Image>();
        showTarget = ShowTarget();
        hideTarget = HideTarget();
        isInteracting = false;
        currentOpacity = targetAim.color.a;
    }

    private void FixedUpdate()
    {       
        if (Tongue.tongueTransform != null && Physics.Raycast(Tongue.tongueTransform.position + Tongue.tongueTransform.forward.normalized/2 , Tongue.tongueTransform.forward.normalized , out hitInfo, maxDistanceAction))
        {
            if(hitInfo.transform.tag.Equals("Tongue") == false && (hitInfo.transform.tag.Equals("Stickable") || hitInfo.transform.tag.Equals("HardSurface")))
            {
                if (!isInteracting)
                {
                    timerToShow = 0;
                    StopCoroutine(hideTarget);
                    showTarget = ShowTarget();
                    StartCoroutine(showTarget);
                    isInteracting = true;
                }
            }
        }        
        else
        {
            if (isInteracting)
            {
                timerToHide = 0;
                StopCoroutine(showTarget);
                hideTarget = HideTarget();
                StartCoroutine(hideTarget);
                isInteracting = false;
            }
        }
    }

    //void OnDrawGizmos()
    //{
    //    Gizmos.DrawRay(Tongue.tongueTransform.position + Tongue.tongueTransform.forward.normalized / 2, Tongue.tongueTransform.forward.normalized * maxDistanceAction);
    //}

    IEnumerator showTarget;
    IEnumerator ShowTarget()
    {
        Color newColor = targetAim.color;
        lastOpacity = currentOpacity;

        while (currentOpacity < 1)
        {            
            timerToShow += Time.deltaTime;
            currentOpacity = Mathf.Lerp(lastOpacity, 1, timerToShow / timeToShowTarget);
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
