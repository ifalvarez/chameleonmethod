using UnityEngine;

public class BirdDectection : MonoBehaviour
{
    public delegate void DetectionEventsDelegate(Transform detectedPosition);
    public event DetectionEventsDelegate OnPlayerDetect;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Jugador" && Tongue.PlayerInvisible == false)
        {
            if(OnPlayerDetect != null)
            {
                OnPlayerDetect(other.transform);
            }
            GameManager.GameOver();
        }
    }
}