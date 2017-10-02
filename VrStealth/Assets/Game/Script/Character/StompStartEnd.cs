using UnityEngine;

public class StompStartEnd : MonoBehaviour
{
    public enum StumpType
    {
        start,
        end
    }
    public StumpType stumpType = StumpType.start;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Jugador" && stumpType == StumpType.end && GameManager.IsGameOver == false)
        {
            Debug.Log("Player Ended");
            GameManager.ClearedLevel();
        }
    }
}