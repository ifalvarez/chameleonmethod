using UnityEngine;

public class TongueTip : MonoBehaviour
{
    public delegate void TongueEventDelegate(Vector3 worldHit);
    public static event TongueEventDelegate OnTongueHit, OnHardHit;

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