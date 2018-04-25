using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenGun : Weapon
{
    GoldenGun()
    {
        firingRate = 4.0f;
        damage = 1000;
        range = 1000.0f;
        clipSize = 1;
        inventorySize = 30;
        reloadTime = 2;
    }


    protected override void ShootMechanic()
    {
        Vector3 recoilPath = new Vector3
            (
            Random.Range(-recoilRangeX, recoilRangeX),
            Random.Range(-recoilRangeY, recoilRangeY),
            0
            );

        RaycastHit hitInfo;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward + (recoilPath / 10), out hitInfo, range))
        {
            // APPLY DAMAGE HERE.
            iKillable killable = hitInfo.collider.GetComponentInParent<iKillable>();
            if (killable != null)
            {
                if (killable.TakeDamage(damage))
                {
                    GoldenGun_GameMode.instance.PlayerKilled(ownerArrayPlace, true);
                }
            }

            if (hitInfo.transform.tag != "Player")
            {
                ObjectPooler.instance.SpawnPooledObject("bulletImpact", hitInfo.point + (hitInfo.normal / 100), Quaternion.LookRotation(hitInfo.normal));
            }
        }
    }
}
