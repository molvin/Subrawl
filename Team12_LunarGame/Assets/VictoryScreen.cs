using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryScreen : MonoBehaviour
{
    //[SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Image _player1Wins;
    [SerializeField] private Image _player2Wins;
    [SerializeField] private float _timeUntilMainMenu;
    [SerializeField] private Image _playerOneWon;
    [SerializeField] private Image _playerTwoWon;

    //private const string Player1WinsText = "<color=blue>Player 1 Wins!";
    //private const string Player2WinsText = "<color=red>Player 2 Wins!";

    private void Start()
    {
        _player1Wins.enabled = _player2Wins.enabled = false;
        GameManager.OnVictory += ShowVictory;
    }
    private void ShowVictory(int id)
    {
        if (id == 0)
            _player1Wins.enabled = true;
        if (id == 1)
            _player2Wins.enabled = true;
        StartCoroutine(GoToMainMenu());
    }

    private IEnumerator GoToMainMenu()
    {
        yield return new WaitForSeconds(_timeUntilMainMenu);
        SceneManager.LoadScene(0);
    }
    
}
