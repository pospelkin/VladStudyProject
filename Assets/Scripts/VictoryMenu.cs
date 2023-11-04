using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryMenu : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public bool isWined = false;
    public void Setup()
    {
        gameObject.SetActive(true);
        isWined = true;
        Time.timeScale= 0f;
        TimeManager timeManager = GameObject.FindObjectOfType<TimeManager>();
        float currentTime = timeManager.currentTime;
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        string timerString = string.Format("{0:00}:{1:00}", minutes, seconds);
        timeText.text = "Time: " + timerString;
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("15");
        Time.timeScale = 1f;
        isWined= false;
    }
}
