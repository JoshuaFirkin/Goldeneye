using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : Weapon
{
    public GameObject projectile;
    public Transform firePoint;
    public GameObject GFX;

    protected override void ShootMechanic()
    {
        GameObject proj = Instantiate(projectile, firePoint.position, firePoint.rotation);
        proj.GetComponent<Projectile>().SetInstigator(this.gameObject, damage);
        muzzleFlash.Play();
        ShowHideGFX(false);
    }

    protected override void OnReloadFinished()
    {
        ShowHideGFX(true);
        base.OnReloadFinished();
    }


    void ShowHideGFX(bool show = true)
    {
        GFX.SetActive(show);
    }
}
