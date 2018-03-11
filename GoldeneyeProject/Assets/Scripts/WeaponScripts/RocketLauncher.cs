using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : ProjectileWeapon
{
    RocketLauncher()
    {
        firingRate = 1.0f;
        damage = 100;
        range = 100;
        clipSize = 1;
        inventorySize = 12;
        reloadTime = 4.0f;
    }
}
