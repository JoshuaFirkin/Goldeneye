using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    Pistol()
    {
        firingRate = 4.0f;
        damage = 10.0f;
        range = 60.0f;
        clipSize = 12;
        inventorySize = 36;
        reloadTime = 2;
    }
}
