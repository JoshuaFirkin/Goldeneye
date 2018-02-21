using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FireType
{
    SEMI_AUTOMATIC,
    FULLY_AUTOMATIC,
    BURST_FIRE
}


public class Weapon : MonoBehaviour
{
    [HideInInspector]
    public Camera cam;
    [HideInInspector]
    public Animator weaponHolderAnim;

    [Header("Stats")]
    public FireType fireType;
    public float firingRate = 1.0f;
    public float damage = 10.0f;
    public float range = 100.0f;
    public int clipSize = 20;
    public int inventorySize = 20;
    public float reloadTime = 3.0f;

    [Header("Effects")]
    public ParticleSystem muzzleFlash;
    public Transform bulletImpact;

    private float timeToFire = 0.0f;
    private int crntClip;
    private int crntInventory;
    private bool canFire = true;

    void Awake()
    {
        crntClip = clipSize;
        crntInventory = inventorySize;
    }


    public virtual void Fire()
    {
        if (Time.time >= timeToFire && crntClip > 0 && canFire)
        {
            timeToFire = Time.time + 1.0f / firingRate;
            ShootRay();
            crntClip--;
            Debug.Log("Current Clip: " + crntClip.ToString());
            muzzleFlash.Play();
        }
    }


    void ShootRay()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, range))
        {
            // APPLY DAMAGE HERE.
            Debug.Log("HIT: " + hitInfo.transform.name);
            ObjectPooler.instance.SpawnPooledObject("bulletImpact", hitInfo.point + (hitInfo.normal / 100), Quaternion.LookRotation(hitInfo.normal));
        }
    }


    public IEnumerator Reload()
    {
        if (crntInventory <= 0 || crntClip == clipSize)
        {
            Debug.Log("No Reload!");
            yield return 0;
        }

        canFire = false;
        yield return new WaitForSeconds(reloadTime);


        if (crntClip + crntInventory < clipSize)
        {
            crntClip = crntClip + crntInventory;
            crntInventory = 0;
        }
        else
        {
            int oldClip = crntClip;
            crntClip = clipSize;
            crntInventory -= (clipSize - oldClip);
        }

        canFire = true;
        Debug.Log(crntClip + " / " + crntInventory);
    }
}
