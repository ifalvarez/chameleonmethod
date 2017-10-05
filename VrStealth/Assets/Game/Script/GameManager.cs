using System.Collections;
using UnityEngine;
using UnityEngine.VR;

public class GameManager : MonoBehaviour
{
    public delegate void ManagerEventsDelegate();
    public static event ManagerEventsDelegate OnGameOver, OnClearLevel;

    static bool canPlay = true;
    public static bool CanPlay { get{return canPlay;} }

    static bool playerCanTongue = false;
    public static bool CanUseTongue { get { return playerCanTongue; } }

    static bool gameOver = false;
    public static bool IsGameOver { get { return gameOver; } }

    static GameManager instance;

    public float timeToRestart = 0.0f;

    private void Awake()
    {
        canPlay = true; //ERASE TEST ONLY
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Application.targetFrameRate = 60;
        VRSettings.renderScale = 1.0f;
        InputTracking.disablePositionalTracking = true;
        OnGameOver += BackToMain;
        OnClearLevel += BackToMain;
        Curtain.OnStarClose += ResetEvents;
        Curtain.OnStarClose += WaitForLoadToInteract;
    }

    void BackToMain ()
    {
        playerCanTongue = false;
        Debug.Log("Getting Back To Menu");
        StartCoroutine(RetryLevel());
    }

    IEnumerator RetryLevel()
    {
        yield return new WaitForSeconds(timeToRestart);
        Curtain.Instance.CloseCurtainAndLoadLevel("MainMenu");
    }

    public static void StartGame()
    {
        canPlay = true;
        playerCanTongue = true;
        gameOver = false;
    }

    void WaitForLoadToInteract()
    {
        playerCanTongue = false;
        canPlay = false;
    }

    public static void ClearedLevel()
    {
        if(OnClearLevel != null)
        {
            OnClearLevel();
        }
        canPlay = false;
        playerCanTongue = false;
        gameOver = true;
    }

    public static void GameOver()
    {
        if(OnGameOver != null)
        {
            OnGameOver();
        }        
        canPlay = false;
        gameOver = true;
    }

    public static void ResetEvents()
    {
        ClearEvents(OnGameOver);
        ClearEvents(OnClearLevel);
        OnGameOver += instance.BackToMain;
        gameOver = false;
    }

    static void ClearEvents(ManagerEventsDelegate events)
    {
        foreach (ManagerEventsDelegate evento in events.GetInvocationList())
        {
            OnGameOver -= evento;
        }        
    }
}