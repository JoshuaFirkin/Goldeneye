using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, iKillable {
    		
    public EnemyController enemyController { get; private set; }

    Vector3 enemySpawnPosition;

    //  enemy properties
    public int enemyHealthPoints { get; private set; }
    public int enemyArmourPoints { get; private set; }    

    private bool isDead = false;

    EnemyHealth()
    {
        enemyHealthPoints = 100;
        enemyArmourPoints = 0;
    }

    void Start()
    {
        enemyController = GetComponent<EnemyController>();

        enemySpawnPosition = GetComponent<Transform>().position;
    }

    /*void enemyPickupArmour(int armourToGive)
    {
        if (enemyArmourPoints + armourToGive >= 100)
        {
            enemyArmourPoints = 100;
        }
        else
        {
            enemyArmourPoints += armourToGive;
        }
    }*/

    bool iKillable.TakeDamage(int amountTaken)
    {
        if (isDead)
        {
            return false;
        }

        if(enemyHealthPoints <= 0)
        {
            enemyDeath();
            return false;
        }        

        enemyHealthPoints -= amountTaken;
        Debug.Log("Enemy took " + amountTaken + " damage!");
        Debug.Log("Enemy's health is now " + enemyHealthPoints);

        if (enemyHealthPoints <= 0)
        {
            enemyDeath();
            return true;
        }
        else
        {
            return false;
        }
    }

    void enemyDeath()
    {
        Debug.Log("Enemy has died!");
        gameObject.SetActive(false);
        isDead = true;
        enemyRespawn(enemySpawnPosition);

    }

    void enemyRespawn(Vector3 enemyRespawnPosition)
    {
        Debug.Log("Enemy has respawned!");
        isDead = false;
        enemyHealthPoints = 100;
        enemyArmourPoints = 0;
        gameObject.transform.position = enemyRespawnPosition;
        gameObject.SetActive(true);        
    }

}
