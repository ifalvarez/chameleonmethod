using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsControll : MonoBehaviour 
{

    private static ButtonsControll instance;
    private string actualSceneName;

	public void LoadScene(string name )
	{
        if(name == "MainMenu" )
            SceneManager.LoadScene(name);
        else
        {
            SceneManager.LoadScene("InterfaceIngame");
            SceneManager.LoadScene(name, LoadSceneMode.Additive);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void RetryScene(GameObject pauseObject)
    {
        if(pauseObject != null)
            pauseObject.GetComponent<PauseBug>().PauseOff();
        actualSceneName = GetActualSceneName();
        SceneManager.UnloadSceneAsync(actualSceneName);
        SceneManager.LoadSceneAsync(actualSceneName, LoadSceneMode.Additive);
    }

    public void NextLevel(string nextLevelName)
    {
        //string nextLevelName=""; //tomar referencia del nivel
        SceneManager.UnloadSceneAsync(GetActualSceneName());
        SceneManager.LoadScene(nextLevelName, LoadSceneMode.Additive);
    }

    private string GetActualSceneName()
	{
		string name;
		name = SceneManager.GetSceneAt(1).name;
		return name;
	}

    private GameObject canvasToShow;
    private GameObject canvasToHide;

    public void CanvasToHide(GameObject canvasToHide)
    {
       this.canvasToHide = canvasToHide;
    }

    public void CanvasToShow(GameObject canvasToShow)
    {
        this.canvasToShow = canvasToShow;
    }

    public void ChangeCanvas()
    {
        canvasToHide.SetActive(false);
        canvasToShow.SetActive(true);
    }

    private bool isPuase;

    public void Pause(GameObject pauseObject)
    {
        pauseObject.GetComponent<PauseBug>().PauseOff();
    }
}
