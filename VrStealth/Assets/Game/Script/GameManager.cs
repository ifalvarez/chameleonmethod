using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void ManagerEventsDelegate();
    public static event ManagerEventsDelegate OnGameOver, OnClearLevel;

    static bool canPlay = false;
    public static bool CanPlay { get{return canPlay;} }

    private void Awake()
    {
        canPlay = true; //ERASE TEST ONLY
    }

    public static void StartGame()
    {
        canPlay = true;
    }

    public static void ClearedLevel()
    {
        if(OnClearLevel != null)
        {
            OnClearLevel();
        }
        canPlay = false;
    }

    public static void GameOver()
    {
        if(OnGameOver != null)
        {
            OnGameOver();
        }        
        canPlay = false;
    }

    public static void ResetGame()
    {
        ClearEvents(OnGameOver);
    }

    static void ClearEvents(ManagerEventsDelegate events)
    {
        foreach(ManagerEventsDelegate evento in events.GetInvocationList())
        {
            OnGameOver -= evento;
        }
    }
}