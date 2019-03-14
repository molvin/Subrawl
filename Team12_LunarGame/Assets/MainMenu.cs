﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("OscarTestScene");
    }

    public void Quit()
    {
        Application.Quit();
    }
}