using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Camera cam;
    public Transform weaponHolder;

    [HideInInspector]
    public Weapon currentWeapon;

    private List<Weapon> inventoryWeapons;
    private int weaponIdx = 0;

    private void Start()
    {
        inventoryWeapons = new List<Weapon>();
    }

    public void PickupWeapon(Weapon weapon)
    {
        Debug.Log("Weapon picked up.");

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
        currentWeapon.transform.localPosition = Vector3.zero;
        currentWeapon.transform.rotation = weaponHolder.rotation;

        currentWeapon.cam = cam;
        currentWeapon.weaponHolderAnim = weaponHolder.GetComponent<Animator>();
        currentWeapon.GetComponent<Collider>().enabled = false;
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
    }
}
