using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    public struct ControllerMap
    {
        public string horAxis;
        public string vertAxis;
        public string horLook;
        public string vertLook;
        public string fire;
        public string reload;
        public string weaponSwitch;
        public string interact;
        public List<string> maps;

        public ControllerMap(RuntimePlatform platform, int playerNumber)
        {
            maps = new List<string>();

            switch (platform)
            {
                case RuntimePlatform.PS4:
                    horAxis = "Horizontal_PS4";
                    vertAxis = "Vertical_PS4";
                    horLook = "LookHorizontal_PS4";
                    vertLook = "LookVertical_PS4";
                    fire = "Fire_PS4";
                    reload = "Reload_PS4";
                    weaponSwitch = "WeaponSwitch_PS4";
                    interact = "Interact_PS4";
                    break;


                case RuntimePlatform.XboxOne:
                    horAxis = "Horizontal_XBONE";
                    vertAxis = "Vertical_XBONE";
                    horLook = "LookHorizontal_XBONE";
                    vertLook = "LookVertical_XBONE";
                    fire = "Fire_XBONE";
                    reload = "Reload_XBONE";
                    weaponSwitch = "WeaponSwitch_XBONE";
                    interact = "Interact_XBONE";
                    break;

                case RuntimePlatform.WindowsPlayer:
                    horAxis = "Horizontal_XBONE";
                    vertAxis = "Vertical_XBONE";
                    horLook = "LookHorizontal_XBONE";
                    vertLook = "LookVertical_XBONE";
                    fire = "Fire_XBONE";
                    reload = "Reload_XBONE";
                    weaponSwitch = "WeaponSwitch_XBONE";
                    interact = "Interact_XBONE";
                    break;


                default:
                    horAxis = "Horizontal_XBONE";
                    vertAxis = "Vertical_XBONE";
                    horLook = "LookHorizontal_XBONE";
                    vertLook = "LookVertical_XBONE";
                    fire = "Fire_XBONE";
                    reload = "Reload_XBONE";
                    weaponSwitch = "WeaponSwitch_XBONE";
                    interact = "Interact_XBONE";
                    break;
            }

            horAxis += ("_P" + playerNumber);
            vertAxis += ("_P" + playerNumber);
            horLook += ("_P" + playerNumber);
            vertLook += ("_P" + playerNumber);
            fire += ("_P" + playerNumber);
            reload += ("_P" + playerNumber);
            weaponSwitch += ("_P" + playerNumber);
            interact += ("_P" + playerNumber);
        }
    }

    private PlayerMotor motor;
    private PlayerInventory inv;
    private Animator anim;
    private ControllerMap ctrlMap;

    public AmmoUI ammoUI;
    public Camera cam;

    public float movementSpeed = 10.0f;
    public float rotationSensitivity = 10.0f;

    private bool inputDisabled = false;
    private int arrayPlace;

    void Start()
    {
        motor = GetComponent<PlayerMotor>();
        inv = GetComponent<PlayerInventory>();
        anim = GetComponent<Animator>();
    }

    public void AssignControllerMap(int playerNumber)
    {
        ctrlMap = new ControllerMap(Application.platform, playerNumber);
    }

    public void SetID(int _arrayPlace)
    {
        arrayPlace = _arrayPlace;
    }

    public int GetID()
    {
        return arrayPlace;
    }

    void Update()
    {
        if (inputDisabled)
        {
            return;
        }

        // Applying movement to the character.
        Vector3 horMovement = transform.right * Input.GetAxis(ctrlMap.horAxis);
        Vector3 verMovement = transform.forward * Input.GetAxis(ctrlMap.vertAxis);

        Vector3 vel = (horMovement + verMovement).normalized * movementSpeed;
        motor.AddMovement(vel);


        // Applying rotation to the character.
        float yRotation = Input.GetAxis(ctrlMap.vertLook);
        Vector3 rotation = new Vector3(0.0f, yRotation, 0.0f) * rotationSensitivity;
        motor.AddRotation(rotation);

        //Applying rotation to the camera.
        float xRotation = Input.GetAxis(ctrlMap.horLook);
        Vector3 camRotation = new Vector3(xRotation, 0.0f, 0.0f) * rotationSensitivity;
        motor.AddCameraRotation(camRotation);

        float timer = anim.GetFloat("reloadTimer");
        if (timer > 0)
        {
            anim.SetFloat("reloadTimer", timer - Time.deltaTime);
        }


        if (inv.currentWeapon != null)
        {
            if (Input.GetAxisRaw(ctrlMap.fire) > 0)
            {
                //Fire the weapon.
                if (inv.currentWeapon.Fire())
                {
                    anim.CrossFadeInFixedTime("Recoil_Anim", 0);
                    ammoUI.UpdateClip(inv.currentWeapon.crntClip);
                }
            }

            if (Input.GetButtonDown(ctrlMap.reload))
            {
                if (inv.currentWeapon.StartReload())
                {
                    anim.SetFloat("reloadTimer", inv.currentWeapon.reloadTime);
                    anim.CrossFadeInFixedTime("Reload_Anim", 0);
                }
            }

            if (Input.GetButtonDown(ctrlMap.weaponSwitch))
            {
                inv.WeaponSwitch();
            }
        }

        if (Input.GetButtonDown(ctrlMap.interact))
        {
            Interact();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Weapon interacted.");
        if (other.GetComponent<Weapon>() != null)
        {
            inv.PickupWeapon(other.gameObject.GetComponent<Weapon>());
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


    public void DisableInput()
    {
        inputDisabled = true;
        motor.StopAllMovement();
    }

    public void EnableInput()
    {
        inputDisabled = false;
    }
}
