using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _livesText;
    [SerializeField] private Button _killPlayer1Button;
    [SerializeField] private Button _killPlayer2Button;
    [SerializeField] private Button _resetLivesButton;

    private const string TextTemplate = "Lives: \n <color=blue>Player1: {0} \n <color=red>Player2: {1}";
    
    private void Start()
    {
        GameManager.OnLivesChanged += UpdatePlayerLives;
        UpdatePlayerLives();
        _killPlayer1Button?.onClick.AddListener(() => PlayerValues.GetPlayer(0)?.Die());
        _killPlayer2Button?.onClick.AddListener(() => PlayerValues.GetPlayer(1)?.Die());
        _resetLivesButton?.onClick.AddListener(GameManager.ResetLives);
    }
    private void UpdatePlayerLives()
    {
        _livesText.text = string.Format(TextTemplate, GameManager.PlayerLives[0], GameManager.PlayerLives[1]);
    }
}
