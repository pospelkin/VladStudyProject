using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Utilities
{
    public static void RestartLevel()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1.0f;
    }

    public static void StopTime()
    {
        Time.timeScale = 0f;
    }
}
