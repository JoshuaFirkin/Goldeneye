using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWeapon : Weapon
{
    public Transform barrel;

    TestWeapon()
    {
        firingRate = 4.0f;
        damage = 10;
        range = 60.0f;
        clipSize = 1000;
        inventorySize = 36;
        reloadTime = 0;
    }


    protected override void ShootRay()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(barrel.transform.position, barrel.transform.forward, out hitInfo, range))
        {
            // APPLY DAMAGE HERE.
            if (hitInfo.transform.tag == "Player")
            {
                Debug.Log(hitInfo.transform.name);
                PlayerHealth playerHP = hitInfo.transform.GetComponentInParent<PlayerHealth>();
                playerHP.TakeDamage(damage);
            }
            ObjectPooler.instance.SpawnPooledObject("bulletImpact", hitInfo.point + (hitInfo.normal / 100), Quaternion.LookRotation(hitInfo.normal));
        }

        Debug.DrawRay(barrel.transform.position, barrel.transform.forward * range, Color.red);
    }
}
