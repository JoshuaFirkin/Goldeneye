using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationUI : MonoBehaviour
{
    public Text notificationText;

    float notificationTime;
    float clearNotificationsTime = 2.0f;
    string pickedupweapon = "KF7 Soviet";


    public void WeaponPickup(string name)
    {
        notificationTime = Time.time;
        notificationText.text = "Picked up a " + name;
        StartCoroutine(StartTimer());
    }


    public void AmmoPickup(int amount)
    {
        notificationTime = Time.time;
        notificationText.text = "Picked up " + amount + " ammo.";
        StartCoroutine(StartTimer());
    }
	

    IEnumerator StartTimer()
    {
        while (Time.time - notificationTime < clearNotificationsTime)
        {
            yield return null;
        }

        notificationText.text = "";
        yield return 0;
    }
}
