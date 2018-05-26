using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour {

    public Transform enemyBarrel;    

    private float timeToFire = 0.0f;
    private bool canFire = true;

    public float enemyFiringRate;
    public int enemyDamage;
    public float enemyRange;
    public int enemyClipSize;
    public int enemyInventorySize;
    public float enemyReloadTime;

    public int enemyCrntClip { get; protected set; }
    public int enemyCrntInventory { get; protected set; }
    public bool outOfAmmo;

    public ParticleSystem enemyMuzzleFlash;

    EnemyWeapon()
    {
        enemyFiringRate = 8.0f;
        enemyDamage = 10;
        enemyRange = 60.0f;
        enemyClipSize = 30;
        enemyInventorySize = 90;
        enemyReloadTime = 3.5f;
    }

    private void Awake()
    {
        enemyCrntClip = enemyClipSize;
        enemyCrntInventory = enemyInventorySize;
    }

    public bool Fire()
    {
        if(Time.time >= timeToFire && enemyCrntClip > 0 && canFire)
        {
            //Debug.Log("Attempting to fire!");
            timeToFire = Time.time + 1.0f / enemyFiringRate;
            ShootMechanic();
            enemyCrntClip--;
            Debug.Log("Enemy's current clip is: " + enemyCrntClip);
            enemyMuzzleFlash.Play();
            return true;
        }
        //Debug.Log("Firing failed!");
        return false;
    }

    protected void ShootMechanic()
    {
        RaycastHit hitInfo;
        if(Physics.Raycast(enemyBarrel.transform.position, enemyBarrel.transform.forward, out hitInfo, enemyRange))
        {
            // damage application here
            iKillable killable = hitInfo.collider.GetComponentInParent<iKillable>();
            if(killable != null)
            {
                killable.TakeDamage(enemyDamage);
            }
            ObjectPooler.instance.SpawnPooledObject("bulletImpact", hitInfo.point + (hitInfo.normal / 100), Quaternion.LookRotation(hitInfo.normal));
        }
        Debug.DrawRay(enemyBarrel.transform.position, enemyBarrel.transform.forward * enemyRange, Color.red);
    }

    public bool StartReload()
    {
        if (enemyCrntInventory <= 0 || enemyCrntClip == enemyClipSize)
        {
            Debug.Log("Enemy no reload!");
            return false;
        }
        else
        {
            StartCoroutine(Reload());
            return true;
        }
    }

    protected virtual IEnumerator Reload()
    {
        canFire = false;
        yield return new WaitForSeconds(enemyReloadTime);

        if (enemyCrntClip + enemyCrntInventory < enemyClipSize)
        {
            enemyCrntClip = enemyCrntClip + enemyCrntInventory;
            enemyCrntInventory = 0;
        }
        else
        {
            int enemyOldClip = enemyCrntClip;
            enemyCrntClip = enemyClipSize;
            enemyCrntInventory -= (enemyClipSize - enemyOldClip);
        }
        canFire = true;
        OnReloadFinished();
    }

    protected virtual void OnReloadFinished()
    {
        canFire = true;
        return;
    }

    public void AddAmmo()
    {
        enemyCrntInventory += enemyClipSize;
    }

}
