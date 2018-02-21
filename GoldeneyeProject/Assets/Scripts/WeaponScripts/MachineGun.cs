using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Weapon
{
    MachineGun()
    {
        weaponOffset = new Vector3(0.6f, -0.26f, 0.86f);
        firingRate = 8.0f;
        damage = 30.0f;
        range = 120.0f;
        clipSize = 30;
        inventorySize = 90;
        reloadTime = 3.5f;
    }
}
