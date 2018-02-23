using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    private int armourPoints = 0;
    private int hitPoints = 100;

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

	public void TakeDamage(int amountTaken)
    {
        if (hitPoints <= 0)
        {
            Debug.Log("DEAD");
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
}
