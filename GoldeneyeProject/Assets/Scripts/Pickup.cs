using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    private MeshRenderer rend;
    private Collider collider;

    [SerializeField] private float waitTime = 15.0f;

    protected void Start()
    {
        rend = GetComponent<MeshRenderer>();
        collider = GetComponent<Collider>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<PlayerController>() != null)
        {
            DoAction(other.gameObject);
            StartCoroutine(DestroyAndReset());
        }
    }

    protected abstract void DoAction(GameObject other);

    protected IEnumerator DestroyAndReset()
    {
        rend.enabled = false;
        collider.enabled = false;

        yield return new WaitForSeconds(waitTime);

        rend.enabled = true;
        collider.enabled = true;
    }
}
