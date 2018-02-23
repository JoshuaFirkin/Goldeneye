using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmourPickup : MonoBehaviour
{
    public int armourAmount = 100;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerHealth player = other.GetComponentInParent<PlayerHealth>();
            player.PickupArmour(armourAmount);
            Destroy(gameObject);
        }
    }

}
