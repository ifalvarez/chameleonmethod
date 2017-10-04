using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CanvasInfo : MonoBehaviour
{
    public Image looseImage, winImage;
    public Image bloodScreen;
    public float bloodScreenFadeSpeed = 0.0f;
    public static CanvasInfo instance;

    private void Awake()
    {
        instance = this;
        looseImage.gameObject.SetActive(false);
        winImage.gameObject.SetActive(false);
        GameManager.OnGameOver += OnGameOver;
        GameManager.OnClearLevel += OnWin;
    }

    void OnGameOver()
    {
        looseImage.gameObject.SetActive(true);
        GameManager.OnGameOver -= OnGameOver;
        GameManager.OnClearLevel -= OnWin;
    }

    public void OnSnakeGameOver()
    {
        StartCoroutine(BleedScreen());
    }

    IEnumerator BleedScreen()
    {
        yield return new WaitForSeconds(0.5f);
        while (true)
        {
            bloodScreen.color = new Color(bloodScreen.color.r, bloodScreen.color.g, bloodScreen.color.b, bloodScreen.color.a + bloodScreenFadeSpeed * Time.deltaTime);
            if (bloodScreen.color.a >= 1.0f)
            {
                break;
            }
            yield return null;
        }
    }

    void OnWin()
    {
        winImage.gameObject.SetActive(true);
        GameManager.OnGameOver -= OnGameOver;
        GameManager.OnClearLevel -= OnWin;
    }    
}
