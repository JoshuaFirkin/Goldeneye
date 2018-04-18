using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComponentReceiver : MonoBehaviour
{
    Transform HUD;

    private void Start()
    {
        HUD = transform.Find("PlayerHUD");
    }

    public AmmoUI GetAmmoUI()
    {
        AmmoUI element = HUD.GetComponent<AmmoUI>();

        if (element != null)
        {
            return element;
        }
        else
        {
            return null;
        }
    }


    public HealthUI GetHealthUI()
    {
        HealthUI element = HUD.GetComponent<HealthUI>();

        if (element != null)
        {
            return element;
        }
        else
        {
            return null;
        }
    }
}
