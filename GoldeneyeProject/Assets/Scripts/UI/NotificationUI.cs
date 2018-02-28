using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationUI : MonoBehaviour {

    public GameObject UINotifications;
    float notificationTime;
    float clearNotificationsTime = 3.0f;
    string pickedupweapon = "KF7 Soviet";


    public void WeaponPickup()
    {
        notificationTime = Time.time;
        UINotifications.GetComponent<Text>().text = "Picked up a " + pickedupweapon;
    }


    public void AmmoPickup()
    {
        notificationTime = Time.time;
        UINotifications.GetComponent<Text>().text = "Picked up some ammo.";
    }
	

	void Update () {
		if (Time.time - notificationTime > clearNotificationsTime)
        {
            UINotifications.GetComponent<Text>().text = "";
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            WeaponPickup();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            AmmoPickup();
        }
    }
}
