using UnityEngine;
using UnityEngine.SceneManagement;

public class TongueTip : MonoBehaviour
{
    public delegate void TongueEventDelegate(Vector3 worldHit);
    public static event TongueEventDelegate OnTongueHit, OnHardHit;

    private void Awake()
    {
        Curtain.OnStarClose += ResetStatics;
    }

    private void ResetStatics()
    {
        if (OnTongueHit != null)
        {
            foreach (TongueEventDelegate reg in OnTongueHit.GetInvocationList())
            {
                OnTongueHit -= reg;
            }
        }

        if (OnHardHit != null)
        {
            foreach (TongueEventDelegate reg in OnHardHit.GetInvocationList())
            {
                OnHardHit -= reg;
            }
        }
        Curtain.OnStarClose -= ResetStatics;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Stickable" && GameManager.CanPlay == true)
        {
            if (OnTongueHit != null)
            {
                OnTongueHit(transform.position);
            }
        }
        else if(other.tag == "HardSurface")
        {
            if(OnHardHit != null)
            {
                OnHardHit(transform.position);
            }
        }
    }
}