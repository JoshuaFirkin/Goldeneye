using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    private PlayerMotor motor;
    private PlayerInventory inv;
    private Animator anim;

    public Camera cam;

    public float movementSpeed = 10.0f;
    public float rotationSensitivity = 10.0f;


    void Start()
    {
        motor = GetComponent<PlayerMotor>();
        inv = GetComponent<PlayerInventory>();
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        // Applying movement to the character.
        Vector3 horMovement = transform.right * Input.GetAxis("Horizontal");
        Vector3 verMovement = transform.forward * Input.GetAxis("Vertical");

        Vector3 vel = (horMovement + verMovement).normalized * movementSpeed;
        motor.AddMovement(vel);


        // Applying rotation to the character.
        float yRotation = Input.GetAxis("LookVertical");
        Vector3 rotation = new Vector3(0.0f, yRotation, 0.0f) * rotationSensitivity;
        motor.AddRotation(rotation);

        //Applying rotation to the camera.
        float xRotation = Input.GetAxis("LookHorizontal");
        Vector3 camRotation = new Vector3(xRotation, 0.0f, 0.0f) * rotationSensitivity;
        motor.AddCameraRotation(camRotation);


        if (inv.currentWeapon != null)
        {
            if (Input.GetAxisRaw("Fire") > 0)
            {
                //Fire the weapon.
                if (inv.currentWeapon.Fire())
                {
                    anim.CrossFadeInFixedTime("Recoil_Anim", 0);
                }
            }

            if (Input.GetButtonDown("Reload"))
            {
                StartCoroutine(inv.currentWeapon.Reload());
            }

            if (Input.GetButtonDown("WeaponSwitch"))
            {
                inv.WeaponSwitch();
            }
        }

        if (Input.GetButtonDown("Interact"))
        {
            Interact();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapon")
        {
            Debug.Log("Weapon interacted.");
            if (other.GetComponent<Weapon>() != null)
            {
                inv.PickupWeapon(other.gameObject.GetComponent<Weapon>());
            }
        }
    }


    void Interact()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, 3))
        {
            // INTERACT WITH OBJECT
            Debug.Log("Interacting with " + hitInfo.transform.name);
            Debug.DrawRay(cam.transform.position, cam.transform.forward * 3, Color.green, 1.0f);
        }
    }
}
