using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    [SerializeField]
    private int damagePerPellet = 20;
    [SerializeField]
    private int pelletsPerShot = 8;

    Shotgun()
    {
        firingRate = 0.8f;
        damage = 0;
        range = 20.0f;
        clipSize = 5;
        inventorySize = 15;
        reloadTime = 5f;

        recoilRangeX = 5.0f;
        recoilRangeY = 5.0f;
    }

    protected override void ShootMechanic()
    {
        for (int i = 1; i < pelletsPerShot + 1; i++)
        {
            Vector3 spread = new Vector3
                (
                Random.Range(-(recoilRangeX / i), recoilRangeX / i),
                Random.Range(-(recoilRangeY / i), recoilRangeY / i),
                0
                );

            RaycastHit hitInfo;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward + (spread / 10), out hitInfo, range))
            {
                // APPLY DAMAGE HERE.
                iKillable killable = hitInfo.collider.GetComponentInParent<iKillable>();
                if (killable != null)
                {
                    if (killable.TakeDamage(damagePerPellet))
                    {
                        GameMode.instance.PlayerKilled(ownerArrayPlace);
                    }
                }

                if (hitInfo.transform.tag != "Player")
                {
                    ObjectPooler.instance.SpawnPooledObject("bulletImpact", hitInfo.point + (hitInfo.normal / 100), Quaternion.LookRotation(hitInfo.normal));
                }
            }
        }
    }

}
