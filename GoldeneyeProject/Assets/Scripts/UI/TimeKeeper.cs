using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeKeeper : MonoBehaviour
{
    private Text timer;
    public GameObject timerObj;

    private Text endGameText;
    public GameObject endGameObj;

    private void Start()
    {
        timer = timerObj.GetComponent<Text>();
        endGameText = endGameObj.GetComponent<Text>();
    }

    public void TurnOffTimer()
    {
        timerObj.SetActive(false);
    }

    public void DisplayTime(int mins, int seconds)
    {
        if (!timerObj.activeInHierarchy)
        {
            return;
        }

        if (seconds < 10)
        {
            timer.text = mins.ToString() + ":0" + seconds.ToString();
        }
        else
        {
            timer.text = mins.ToString() + ":" + seconds.ToString();
        }
    }

    public void DisplayEndGame()
    {
        endGameText.text = "Game Over!";
    }
}
