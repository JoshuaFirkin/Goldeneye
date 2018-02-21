using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<Weapon> inventoryWeapons;
    public Camera cam;

    [HideInInspector]
    public Weapon currentWeapon;

    private int weaponIdx = 0;


    public void PickupWeapon(Weapon weapon)
    {
        Debug.Log("Weapon picked up.");

        inventoryWeapons.Add(weapon);

        currentWeapon = weapon;
        currentWeapon.transform.SetParent(cam.transform);
        currentWeapon.transform.localRotation = Quaternion.Euler(Vector3.zero);
        currentWeapon.transform.localPosition = currentWeapon.weaponOffset;

        currentWeapon.cam = cam;
        currentWeapon.GetComponent<Collider>().enabled = false;
    }


    public void WeaponSwitch()
    {
        currentWeapon.gameObject.SetActive(false);
        if (weaponIdx == inventoryWeapons.Count)
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
