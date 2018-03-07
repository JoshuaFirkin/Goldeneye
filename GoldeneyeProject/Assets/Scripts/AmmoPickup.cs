using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : Pickup
{
    protected override void DoAction(GameObject other)
    {
        Weapon weapon = other.GetComponentInParent<PlayerInventory>().currentWeapon;
        if (weapon != null)
        {
            weapon.AddAmmo();
            Destroy(gameObject);
        }
    }
}
