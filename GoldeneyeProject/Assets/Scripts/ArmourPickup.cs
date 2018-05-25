using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmourPickup : Pickup
{
    public int armourAmount = 100;

    protected override void DoAction(GameObject other)
    {
        PlayerHealth player = other.GetComponentInParent<PlayerHealth>();
        player.PickupArmour(armourAmount);
    }

}
