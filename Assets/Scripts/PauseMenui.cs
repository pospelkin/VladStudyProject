using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenui : MonoBehaviour
{
    public GameObject pauseGameMenu;
    public bool isPaused = false;

    public void Resume()
    {
        pauseGameMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void PauseGame()
    {
        pauseGameMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

}