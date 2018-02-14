using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    private PlayerMotor motor;

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
    }
}
