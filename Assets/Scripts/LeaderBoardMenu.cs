using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoardMenu : MonoBehaviour
{
    public GameObject leaderBoard;
    public GameObject optionsPanel;

    public bool isOpenedLeaderBoard = false;
    public bool isOpenedOptions = false;

    public void Resume()
    {
        leaderBoard.SetActive(false);
        Time.timeScale = 1f;
        isOpenedLeaderBoard = false;
    }

    public void PauseGame()
    {
        leaderBoard.SetActive(true);
        Time.timeScale = 0f;
        isOpenedLeaderBoard = true;
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
        Time.timeScale = 1f;
        isOpenedOptions = false;
    }

    public void OpenOptions()
    {
        optionsPanel.SetActive(true);
        Time.timeScale = 0f;
        isOpenedOptions = true;
    }
}
