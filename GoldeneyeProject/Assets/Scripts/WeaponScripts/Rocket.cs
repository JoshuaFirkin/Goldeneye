using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Projectile
{
    Rocket()
    {
        speed = 30.0f;
        gravityMultiplier = 0.5f;
        expRadius = 1.0f;
    }
}
