using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [HideInInspector]
    public Camera cam;

    [Header("Transforms")]
    public Vector3 weaponOffset = Vector3.zero;

    [Header("Stats")]
    public float firingRate = 1.0f;
    public float damage = 10.0f;
    public float range = 100.0f;

    [Header("Effects")]
    public ParticleSystem muzzleFlash;

    private float timeToFire = 0.0f;

    public virtual void Fire()
    {
        Debug.Log("PEW! PEW!");
        if (Time.time >= timeToFire)
        {
            timeToFire = Time.time + 1.0f / firingRate;
            ShootRay();
            muzzleFlash.Play();
        }

        return;
    }

    void ShootRay()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, range))
        {
            // APPLY DAMAGE HERE.
            Debug.Log("HIT: " + hitInfo.transform.name);
        }
    }
}
