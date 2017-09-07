using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour 
{

    private static LoadLevel instance;
    private string actualSceneName;

    void Awake()
    {        
        if (instance == null)
            instance = this;
        else
            DestroyImmediate(gameObject);

        DontDestroyOnLoad(this.gameObject);
    }

	public void LoadScene(string name )
	{
        if(name == "MainMenu" )
            SceneManager.LoadScene(name);
        else
        {
            SceneManager.LoadScene(name, LoadSceneMode.Additive);
            SceneManager.LoadScene("Pause", LoadSceneMode.Additive);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void RetryScene()
    {
        actualSceneName = GetActualSceneName();
        SceneManager.UnloadSceneAsync(actualSceneName);
        SceneManager.LoadSceneAsync(actualSceneName, LoadSceneMode.Additive);
    }

    public void NextLevel()
    {
        string nextLevelName=""; //tomar referencia del nivel
        SceneManager.UnloadSceneAsync(actualSceneName);
        SceneManager.LoadSceneAsync(nextLevelName, LoadSceneMode.Additive);
    }

    private string GetActualSceneName()
	{
		string name;
		name = SceneManager.GetSceneAt(0).name;
		return name;
	}
}
