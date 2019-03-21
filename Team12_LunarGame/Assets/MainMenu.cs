using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public string GameplaySceneName = "Gameplay";

    
    private void Start()
    {
        AudioManager.PlaySound("Ambiance");
    }
    
    public void PlayGame()
    {
        SceneManager.LoadScene(GameplaySceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
