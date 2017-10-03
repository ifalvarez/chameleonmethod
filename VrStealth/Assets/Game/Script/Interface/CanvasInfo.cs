using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CanvasInfo : MonoBehaviour
{
    public Image looseImage, winImage;
    public Image bloodScreen;
    public float bloodScreenFadeSpeed = 0.0f;

    private void Awake()
    {
        looseImage.gameObject.SetActive(false);
        winImage.gameObject.SetActive(false);
        GameManager.OnGameOver += OnGameOver;
        GameManager.OnClearLevel += OnWin;
    }

    void OnGameOver()
    {
        looseImage.gameObject.SetActive(true);
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
    }    
}
