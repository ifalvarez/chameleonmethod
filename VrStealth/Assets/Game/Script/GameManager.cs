﻿using UnityEngine;
//using UnityEngine.VR;

public class GameManager : MonoBehaviour
{
    public delegate void ManagerEventsDelegate();
    public static event ManagerEventsDelegate OnGameOver, OnClearLevel;

    static bool canPlay = false;
    public static bool CanPlay { get{return canPlay;} }

    static bool playerCanTongue = false;
    public static bool CanUseTongue { get { return playerCanTongue; } }

    private void Awake()
    {
        canPlay = true; //ERASE TEST ONLY
        //Application.targetFrameRate = 60;
        //VRSettings.renderScale = 1.0f;
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