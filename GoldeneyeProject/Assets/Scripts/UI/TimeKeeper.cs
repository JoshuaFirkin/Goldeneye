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

    private Text framesText;
    public GameObject framesObj;

    private float secs = 0;

    private void Start()
    {
        timer = timerObj.GetComponent<Text>();
        endGameText = endGameObj.GetComponent<Text>();
        framesText = framesObj.GetComponent<Text>();
    }

    private void Update()
    {
        secs += Time.deltaTime;
        if (secs > 0.5f)
        {
            secs = 0;

            float frames = 1.0f / Time.deltaTime;
            int framesInt = Mathf.RoundToInt(frames);
            framesText.text = framesInt + " FPS";
        }
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
