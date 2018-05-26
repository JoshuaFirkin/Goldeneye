using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInventory : MonoBehaviour {

    public Transform enemyWeaponHolder;
    public EnemyController controller { get; private set; }        
    public EnemyWeapon enemyCurrentWeapon;        

    private void Start()
    {
        controller = GetComponent<EnemyController>();               
    }

    public void enemyPickupWeapon()
    {
        enemyCurrentWeapon.AddAmmo();                     
    }
    
    public void ResetWeapon()
    {
        enemyCurrentWeapon = null;        
    }
}
