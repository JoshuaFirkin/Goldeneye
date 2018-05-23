using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeKeeper : MonoBehaviour
{
    private Text timer;
    public GameObject timerObj;

    private void Start()
    {
        timer = timerObj.GetComponent<Text>();
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

        timer.text = mins.ToString() + ":" + seconds.ToString();
    }
}
