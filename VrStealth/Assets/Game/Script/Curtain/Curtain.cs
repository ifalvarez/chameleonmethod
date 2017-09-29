using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
[RequireComponent(typeof(CanvasScaler))]
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
    float auxCurtainTime = 0.0f;

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
        auxCurtainTime = timeToCloseCurtain + Time.time;
        Color newColor = curtainBackground.color;
        newColor.a = 0.0f;

        while (true)
        {
            newColor.a = 1.0f - ((auxCurtainTime - Time.time) * 1.0f / timeToCloseCurtain);
            if (newColor.a >= 1.0f)
            {
                newColor.a = 1.0f;
                if(OnClose != null)
                {
                    OnClose();
                }
                ClearEventRegistry();
                SceneManager.LoadSceneAsync(levelToLoad);
                curtainBackground.color = newColor;
                Debug.Log("Closed Curtain");
                break;
            }
            curtainBackground.color = newColor;
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
        auxCurtainTime = timeToCloseCurtain + Time.time;
        Color newColor = curtainBackground.color;
        while (true)
        {
            newColor.a = (auxCurtainTime - Time.time) * 1.0f / timeToCloseCurtain;
            if (newColor.a <= 0.0f)
            {
                curtainBackground.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
                if (OnOpen != null)
                {
                    OnOpen();
                }
                curtainBackground.color = newColor;
                Debug.Log("Opened Curtain");
                break;
            }
            curtainBackground.color = newColor;
            yield return null;
        }
    }
}