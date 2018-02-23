using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Weapon
{
    MachineGun()
    {
        firingRate = 8.0f;
        damage = 30;
        range = 120.0f;
        clipSize = 30;
        inventorySize = 90;
        reloadTime = 3.5f;
    }
}
