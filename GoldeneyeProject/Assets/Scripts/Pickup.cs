using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            DoAction(other.gameObject);
        }
    }

    protected abstract void DoAction(GameObject other);
}
