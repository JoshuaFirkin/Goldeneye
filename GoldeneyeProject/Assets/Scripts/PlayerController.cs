using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    private PlayerMotor motor;
    private Weapon currentWeapon;

    public Camera cam;

    public float movementSpeed = 10.0f;
    public float rotationSensitivity = 10.0f;


    void Start()
    {
        motor = GetComponent<PlayerMotor>();
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


        if (currentWeapon != null)
        {
            if (Input.GetAxisRaw("Fire") > 0)
            {
                //Fire the weapon.
                currentWeapon.Fire();
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapon")
        {
            Debug.Log("Weapon interacted.");
            if (other.GetComponent<Weapon>() != null)
            {
                PickupWeapon(other.gameObject.GetComponent<Weapon>());
            }
        }
    }


    public void PickupWeapon(Weapon weapon)
    {
        Debug.Log("Weapon picked up.");

        currentWeapon = weapon;
        currentWeapon.transform.SetParent(cam.transform);
        currentWeapon.transform.localRotation = Quaternion.Euler(Vector3.zero);
        currentWeapon.transform.localPosition = currentWeapon.weaponOffset;

        currentWeapon.cam = cam;
        currentWeapon.GetComponent<Collider>().enabled = false;
    }
}
