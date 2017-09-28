using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(Canvas))]
[RequireComponent(typeof(CanvasScaler))]
[RequireComponent(typeof(GraphicRaycaster))]
public class Curtain : MonoBehaviour
{
    public delegate void CurtainEventsDelegate();
    public static event CurtainEventsDelegate OnClose, OnOpen;

    public static Curtain Instance;

    public Image curtainBackground;
    public float timeToCloseCurtain = 0.0f;
    public float startDelay = 0.0f;

    IEnumerator closeCurtainMethod, openCurtainMethod;
    string levelToLoad;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        SceneManager.sceneLoaded += OpenCurtainAndContinue;
        curtainBackground.color = new Color(curtainBackground.color.r, curtainBackground.color.g, curtainBackground.color.b, 1.0f);
        StartCoroutine(StartDelay());
    }

    private IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(startDelay);
        StartCoroutine(OpenCurtain());
    }

    public void ClearEventRegistry()
    {
        if (OnClose != null)
        {
            foreach (CurtainEventsDelegate reg in OnClose.GetInvocationList())
            {
                OnClose -= reg;
            }
        }

        if (OnOpen != null)
        {
            foreach (CurtainEventsDelegate reg in OnOpen.GetInvocationList())
            {
                OnOpen -= reg;
            }
        }
    }

    public void CloseCurtainAndLoadLevel(string levelName)
    {
        levelToLoad = levelName;
        closeCurtainMethod = CloseCurtain();
        StartCoroutine(closeCurtainMethod);
    }    

    IEnumerator CloseCurtain()
    {
        yield return new WaitForSeconds(startDelay);
        float delta = 0.0f;
        while (true)
        {
            delta += Time.deltaTime;
            curtainBackground.color += new Color(0.0f, 0.0f, 0.0f, delta / timeToCloseCurtain);
            if(curtainBackground.color.a >= 1.0f)
            {
                curtainBackground.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
                if(OnClose != null)
                {
                    OnClose();
                }
                ClearEventRegistry();
                SceneManager.LoadSceneAsync(levelToLoad);
                Debug.Log("Curtain Close");                
                break;
            }
            yield return null;
        }
    }

    void OpenCurtainAndContinue(Scene sceneLoadedInfo, LoadSceneMode mode)
    {
        if (sceneLoadedInfo.name == levelToLoad)
        {
            openCurtainMethod = OpenCurtain();
            StartCoroutine(openCurtainMethod);
        }
    }

    IEnumerator OpenCurtain()
    {
        float delta = 0.0f;
        Color newColor = curtainBackground.color;
        while (true)
        {
            delta += Time.deltaTime;
            newColor.a = Mathf.Lerp(newColor.a, 0.0f, delta / timeToCloseCurtain);
            if (curtainBackground.color.a <= 0.0f)
            {
                curtainBackground.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
                if (OnOpen != null)
                {
                    OnOpen();
                }
                Debug.Log("Curtain Open");
                break;
            }
            curtainBackground.color = newColor;
            yield return null;
        }
    }
}
