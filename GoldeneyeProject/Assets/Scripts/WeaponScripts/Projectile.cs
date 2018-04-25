using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Projectile : MonoBehaviour
{
    PlayerController instigator;
    int instDamage;

    public float speed = 10.0f;
    public float gravityMultiplier = 1.0f;
    public float expRadius = 1.0f;

    private Rigidbody rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 5.0f);
    }


    private void FixedUpdate()
    {
        rb.AddForce(transform.forward * speed * Time.fixedDeltaTime * (20 - (rb.velocity.magnitude * 2)));
        rb.AddForce(-Vector3.up * (gravityMultiplier / 10));
    }


    private void OnCollisionEnter(Collision collision)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, expRadius);

        foreach (Collider collider in hitColliders)
        {
            Debug.Log("Looping");
            iKillable killable = collider.GetComponentInParent<iKillable>();
            if (killable != null)
            {
                Debug.Log("Doing " + instDamage + " damage.");

                if (killable.TakeDamage(instDamage))
                {
                    GameMode.instance.PlayerKilled(instigator.GetID());
                }
            }
        }

        
        Destroy(gameObject);
    }


    public void SetInstigator(GameObject _player, int _damage)
    {
        instigator = _player.GetComponent<PlayerController>();
        instDamage = _damage;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, expRadius);
    }
}
