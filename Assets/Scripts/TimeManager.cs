using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.ComponentModel;

public class TimeManager : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI bestTimeText;

    public float currentTime;
    public float[] bestTimes = new float[10];

    void Start()
    {
        //PlayerPrefs.DeleteAll();
        for (int i = 0; i < 10; i++)
        {
            bestTimes[i] = PlayerPrefs.GetFloat("BestTime" + i, 0);
        }
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        string timerString = string.Format("{0:00}:{1:00}", minutes, seconds);
        timeText.text = "Time: " + timerString;

        if (bestTimes[0] > 0)
        {
            int bestMinutes = Mathf.FloorToInt(bestTimes[0] / 60);
            int bestSeconds = Mathf.FloorToInt(bestTimes[0] % 60);
            string bestTimeString = string.Format("{0:00}:{1:00}", bestMinutes, bestSeconds);
            bestTimeText.text = "Best time: " + bestTimeString;
        }
    }

    public void SaveCurrentTime()
    {
        for (int i = 0; i < 10; i++)
        {
            if (currentTime < bestTimes[i] || bestTimes[i] == 0)
            {
                float tempTime = bestTimes[i];
                bestTimes[i] = currentTime;

                for (int j = i + 1; j < 10; j++)
                {
                    float temp = bestTimes[j];
                    bestTimes[j] = tempTime;
                    tempTime = temp;
                }

                for (int j = 0; j < 10; j++)
                {
                    PlayerPrefs.SetFloat("BestTime" + j, bestTimes[j]);
                }
                PlayerPrefs.Save();
                break;
            }
        }
    }
}
