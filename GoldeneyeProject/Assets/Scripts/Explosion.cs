using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    public float delay = 3f;

    public float radius = 5f;

    public float force = 700f;

    private int desHealth = 100;

    public GameObject explosionEffect;

    //Functions same as health loss
    public void Destruct(int amountTaken)
    {
        
        if (desHealth <= 0)
        {
            Debug.Log("Time to Die");
            Explode();
            return;
        }

        desHealth -= amountTaken;
        Debug.Log("Des took " + amountTaken + " damage!");
        Debug.Log("Des HP is now " + desHealth);

    }


    public void Explode()
    {
        Debug.Log("BOOM");
        //show effect
        Instantiate(explosionEffect, transform.position, transform.rotation);
        //Creates a sphere around object to see what explodes 
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        //Forces the objects outwards from the explosion. Will require tweaking and damage
        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }
        }
        //Destorys the object once exploded
        Destroy(gameObject);
       
    }
}
