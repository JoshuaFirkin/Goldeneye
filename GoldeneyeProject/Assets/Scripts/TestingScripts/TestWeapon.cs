using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWeapon : Weapon
{
    public Transform barrel;

    private float timeToFire = 0.0f;
    private bool canFire = true;

    TestWeapon()
    {
        firingRate = 4.0f;
        damage = 10;
        range = 60.0f;
        clipSize = 1000;
        inventorySize = 36;
        reloadTime = 0;
    }

    public override bool Fire()
    {
        if (Time.time >= timeToFire && crntClip > 0 && canFire)
        {
            timeToFire = Time.time + 1.0f / firingRate;
            ShootMechanic();
            muzzleFlash.Play();
            return true;
        }

        return false;
    }

    protected override void ShootMechanic()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(barrel.transform.position, barrel.transform.forward, out hitInfo, range))
        {
            // APPLY DAMAGE HERE.
            iKillable killable = hitInfo.collider.GetComponentInParent<iKillable>();
            if (killable != null)
            {
                killable.TakeDamage(damage);
            }

            ObjectPooler.instance.SpawnPooledObject("bulletImpact", hitInfo.point + (hitInfo.normal / 100), Quaternion.LookRotation(hitInfo.normal));
        }

        Debug.DrawRay(barrel.transform.position, barrel.transform.forward * range, Color.red);
    }
}
