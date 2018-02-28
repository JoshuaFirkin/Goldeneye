using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerHealth : MonoBehaviour, iKillable
{
    public PlayerController controller { get; private set; }
    public HealthUI healthUI;

    public int armourPoints { get; private set; }
    public int hitPoints { get; private set; }

    private bool isDead = false;

    PlayerHealth()
    {
        hitPoints = 100;
        armourPoints = 0;
    }

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

        healthUI.UpdateArmour((float)armourPoints);
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
                healthUI.UpdateArmour((float)armourPoints);
                Debug.Log("Armour took " + (armourPoints - amountTaken) + " damage! Armour now at " + armourPoints);
                return;
            }
            else
            {
                amountTaken -= armourPoints;
                armourPoints = 0;
                healthUI.UpdateArmour((float)armourPoints);
                Debug.Log("Armour broke!");
            }
        }

        hitPoints -= amountTaken;
        Debug.Log("You took " + amountTaken + " damage!");
        Debug.Log("Your HP is now " + hitPoints);

        healthUI.UpdateHealth((float)hitPoints);
    }


    void Death()
    {
        // Add End Game Here! Access GameMaster from here.
        Debug.Log("DED");
        isDead = true;
        controller.DisableInput();
    }
}
