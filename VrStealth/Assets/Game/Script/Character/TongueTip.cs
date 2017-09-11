using UnityEngine;

public class TongueTip : MonoBehaviour
{
    public delegate void TongueEventDelegate(Vector3 worldHit);
    public static event TongueEventDelegate OnTongueHit;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit Something");
        if (other.tag != "Jugador")
        {
            if (OnTongueHit != null)
            {
                OnTongueHit(transform.position);
            }
        }
    }
}
