using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerHealth : MonoBehaviour, iKillable
{
    public PlayerController controller { get; private set; }

    private int armourPoints = 0;
    private int hitPoints = 100;
    private bool isDead = false;


    void Start()
    {
        controller = GetComponent<PlayerController>();
    }

    public void PickupArmour(int armourToGive)
    {
        if (armourPoints + armourToGive >= 100)
        {
            armourPoints = 100;
        }
        else
        {
            armourPoints += armourToGive;
        }

        Debug.Log("You picked up armour!" + armourPoints);
    }


    void iKillable.TakeDamage(int amountTaken)
    {
        if (isDead)
        {
            return;
        }

        if (hitPoints <= 0)
        {
            Death();
            return;
        }

        if (armourPoints > 0)
        {
            if (armourPoints - amountTaken >= 0)
            {
                armourPoints -= amountTaken;
                Debug.Log("Armour took " + (armourPoints - amountTaken) + " damage! Armour now at " + armourPoints);
                return;
            }
            else
            {
                amountTaken -= armourPoints;
                armourPoints = 0;
                Debug.Log("Armour broke!");
            }
        }

        hitPoints -= amountTaken;
        Debug.Log("You took " + amountTaken + " damage!");
        Debug.Log("Your HP is now " + hitPoints);
    }


    void Death()
    {
        // Add End Game Here! Access GameMaster from here.
        Debug.Log("DED");
        isDead = true;
        controller.DisableInput();
    }
}
