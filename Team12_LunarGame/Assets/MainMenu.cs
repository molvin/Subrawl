using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public string GameplaySceneName = "Gameplay";
    [SerializeField] private ButtonWithActions _playButton;
    [SerializeField] private ButtonWithActions _quitButton;

    [SerializeField] private Sprite[] _playSprites;
    [SerializeField] private Sprite[] _quitSprites;
    
    
    private void Start()
    {
        AudioManager.PlaySound("Ambiance");
        _playButton.OnHoverEnter += () => _playButton.image.sprite = _playSprites[1];
        _playButton.OnHoverExit += () => _playButton.image.sprite = _playSprites[0];
        _playButton.OnClickBegin += () => _playButton.image.sprite = _playSprites[2];
        _playButton.OnClickEnd += () => _playButton.image.sprite = _playSprites[1];
        
        _quitButton.OnHoverEnter += () => _quitButton.image.sprite = _quitSprites[1];
        _quitButton.OnHoverExit += () => _quitButton.image.sprite = _quitSprites[0];
        _quitButton.OnClickBegin += () => _quitButton.image.sprite = _quitSprites[2];
        _quitButton.OnClickEnd += () => _quitButton.image.sprite = _quitSprites[1];
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
