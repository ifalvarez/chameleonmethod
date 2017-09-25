using UnityEngine;

public class BirdDectection : MonoBehaviour
{
    public delegate void DetectionEventsDelegate(Transform detectedPosition);
    public event DetectionEventsDelegate OnPlayerDetect;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Jugador")
        {
            if(OnPlayerDetect != null)
            {
                OnPlayerDetect(other.transform);
            }
            Debug.Log("Found Player");
        }
    }
}