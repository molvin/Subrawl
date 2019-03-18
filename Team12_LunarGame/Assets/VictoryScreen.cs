using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private float _timeUntilMainMenu;

    private const string Player1WinsText = "<color=blue>Player 1 Wins!";
    private const string Player2WinsText = "<color=red>Player 2 Wins!";

    private void Start()
    {
        _text.enabled = false;
        GameManager.OnVictory += ShowVictory;
    }
    private void ShowVictory(int id)
    {
        _text.text = id == 0 ? Player1WinsText : Player2WinsText;
        _text.enabled = true;
        StartCoroutine(GoToMainMenu());
    }

    private IEnumerator GoToMainMenu()
    {
        yield return new WaitForSeconds(_timeUntilMainMenu);
        SceneManager.LoadScene(0);
    }
    
}
