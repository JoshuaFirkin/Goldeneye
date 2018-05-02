﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [Header("Externals")]
    public Camera cam;
    public Transform weaponHolder;
    public AmmoUI ammoUI;
    public NotificationUI notifUI;
    public PlayerController controller { get; private set; }

    [Header("Audio")]
    public AudioClip pickupClip;
    public AudioClip switchClip;
    [SerializeField] private PlayerAudio playerAudio;

    [HideInInspector]
    public Weapon currentWeapon;

    private List<Weapon> inventoryWeapons;
    private int weaponIdx = 0;

    private void Start()
    {
        controller = GetComponent<PlayerController>();
        inventoryWeapons = new List<Weapon>();
    }

    public void PickupWeapon(Weapon weapon)
    {
        inventoryWeapons.Add(weapon);

        if (currentWeapon != null)
        {
            currentWeapon.gameObject.SetActive(false);
        }

        if (weaponIdx >= inventoryWeapons.Count - 1)
        {
            weaponIdx = 0;
        }
        else
        {
            weaponIdx++;
        }

        currentWeapon = weapon;
        currentWeapon.transform.SetParent(weaponHolder);
        currentWeapon.transform.localPosition = currentWeapon.weaponOffset;
        currentWeapon.transform.rotation = weaponHolder.rotation;

        currentWeapon.cam = cam;
        currentWeapon.weaponHolderAnim = weaponHolder.GetComponent<Animator>();
        currentWeapon.GetComponent<Collider>().enabled = false;

        iHoldable weaponPickup = currentWeapon;
        weaponPickup.OnPickup(controller.GetID());

        ammoUI.UpdateImage(currentWeapon.bulletImage);
        ammoUI.UpdateClipAndInventory(currentWeapon.crntClip, currentWeapon.crntInventory);

        notifUI.WeaponPickup(currentWeapon.weaponName);

        playerAudio.PlayAttachedAudio(pickupClip);

        controller.GFXAnim.SetInteger("weapon", currentWeapon.weaponID);
    }


    public void WeaponSwitch()
    {
        currentWeapon.gameObject.SetActive(false);
        if (weaponIdx >= inventoryWeapons.Count - 1)
        {
            weaponIdx = 0;
        }
        else
        {
            weaponIdx++;
        }

        currentWeapon = inventoryWeapons[weaponIdx];
        currentWeapon.gameObject.SetActive(true);

        ammoUI.UpdateImage(currentWeapon.bulletImage);
        ammoUI.UpdateClipAndInventory(currentWeapon.crntClip, currentWeapon.crntInventory);

        playerAudio.PlayAttachedAudio(switchClip);
    }

    public void ResetWeapons()
    {
        currentWeapon = null;
        inventoryWeapons.Clear();

        Weapon[] children = weaponHolder.GetComponentsInChildren<Weapon>();

        foreach (Weapon child in children)
        {
            if (child.specialWeapon)
            {
                GoldenGun_GameMode.instance.SpawnGoldenGun();
            }

            DestroyImmediate(child.gameObject);
        }
    }
}
