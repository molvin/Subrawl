using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public string GameplaySceneName = "Gameplay";

    public void PlayGame()
    {
        SceneManager.LoadScene(GameplaySceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
