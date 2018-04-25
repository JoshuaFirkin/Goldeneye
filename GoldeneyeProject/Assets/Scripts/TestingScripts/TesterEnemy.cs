using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesterEnemy : MonoBehaviour, iKillable
{
    public Weapon weapon;

    void Start()
    {
        StartCoroutine(TestFire());
    }

    IEnumerator TestFire()
    {
        while (true)
        {
            weapon.Fire();
            yield return new WaitForEndOfFrame();
        }
    }

    bool iKillable.TakeDamage(int amountTaken)
    {
        Debug.Log("Test Damage Taken: " + amountTaken);
        return false;
    }
}
