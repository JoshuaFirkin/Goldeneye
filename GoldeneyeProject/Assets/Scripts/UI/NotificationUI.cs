using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationUI : MonoBehaviour {

    public GameObject UINotifications;
    float notificationtime;
    float clearnotificationstime = 3.0f;
    string pickedupweapon = "KF7 Soviet";

    public void WeaponPickup()
    {
        notificationtime = Time.time;
        UINotifications.GetComponent<Text>().text = "Picked up a " + pickedupweapon;
    }

    public void AmmoPickup()
    {
        notificationtime = Time.time;
        UINotifications.GetComponent<Text>().text = "Picked up some ammo.";
    }
	
	// Update is called once per frame
	void Update () {
		if (Time.time - notificationtime > clearnotificationstime)
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
