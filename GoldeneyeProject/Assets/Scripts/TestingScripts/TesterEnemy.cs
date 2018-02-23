using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesterEnemy : MonoBehaviour
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
}
