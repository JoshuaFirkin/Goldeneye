using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField]
    private Camera cam;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 camRotation = Vector3.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void AddMovement(Vector3 vel)
    {
        velocity = vel;
    }

    public void AddRotation(Vector3 rot)
    {
        rotation = rot;
    }

    public void AddCameraRotation(Vector3 rot)
    {
        camRotation = rot;
    }

    void FixedUpdate()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + (velocity * Time.fixedDeltaTime));
        }

        if (rotation != Vector3.zero)
        {
            rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation * Time.fixedDeltaTime));
        }

        if (camRotation != Vector3.zero)
        {
            if (cam != null)
            {
                cam.transform.Rotate(-camRotation * Time.deltaTime);
            }
        }
    }
}
