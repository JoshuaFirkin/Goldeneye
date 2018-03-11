using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FireType
{
    SEMI_AUTOMATIC,
    FULLY_AUTOMATIC,
    BURST_FIRE
}


public class Weapon : MonoBehaviour, iHoldable
{
    [HideInInspector]
    public Camera cam;
    [HideInInspector]
    public Animator weaponHolderAnim;

    public AmmoUI ammoUI;

    [Header("Stats")]
    public string weaponName;
    public Vector3 weaponOffset = Vector3.zero;
    public Sprite bulletImage;
    public float firingRate = 1.0f;
    public int damage = 10;
    public float range = 100.0f;
    public int clipSize = 20;
    public int inventorySize = 20;
    public float reloadTime = 3.0f;
    [Range(0,1)]
    public float recoilRangeX = 0.0f;
    [Range(0, 1)]
    public float recoilRangeY = 0.0f;

    [Header("Effects")]
    public ParticleSystem muzzleFlash;

    public GameObject owner { get; private set; }
    public int crntClip { get; private set; }
    public int crntInventory { get; private set; }

    private float timeToFire = 0.0f;
    private bool canFire = true;

    void Awake()
    {
        crntClip = clipSize;
        crntInventory = inventorySize;
    }


    public virtual bool Fire()
    {
        if (Time.time >= timeToFire && crntClip > 0 && canFire)
        {
            timeToFire = Time.time + 1.0f / firingRate;
            ShootMechanic();
            crntClip--;
            muzzleFlash.Play();
            return true;
        }

        return false;
    }


    protected virtual void ShootMechanic()
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
                killable.TakeDamage(damage);
            }

            if (hitInfo.transform.tag != "Player")
            {
                ObjectPooler.instance.SpawnPooledObject("bulletImpact", hitInfo.point + (hitInfo.normal / 100), Quaternion.LookRotation(hitInfo.normal));
            }
        }
    }


    public bool StartReload()
    {
        if (crntInventory <= 0 || crntClip == clipSize)
        {
            Debug.Log("No Reload!");
            return false;
        }
        else
        {
            StartCoroutine(Reload());
            return true;
        }
    }

    IEnumerator Reload()
    {
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
        ammoUI.UpdateClipAndInventory(crntClip, crntInventory);
        OnReloadFinished();

        Debug.Log(crntClip + " / " + crntInventory);
    }


    protected virtual void OnReloadFinished()
    {
        return;
    }


    public void AddAmmo()
    {
        crntInventory += clipSize;
        ammoUI.UpdateInventory(crntInventory);
    }

    void iHoldable.OnPickup()
    {
        // Does nothing right now, but if you need a weapon to do something when you pick it up. pop it here.
        Debug.Log("Weapon Pickup");
        return;
    }
}
