using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairMove : MonoBehaviour {

    Vector3 MoveVector;
    public float MoveSpeed = 5.0f;

    public string horString, vertString;

	// Use this for initialization
	void Start ()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.PS4:
                horString = "Horizontal_PS4_P1";
                vertString = "Vertical_PS4_P1";
                break;

            case RuntimePlatform.XboxOne:
                horString = "Horizontal_XBONE_P1";
                vertString = "Vertical_XBONE_P1";
                break;

            default:
                horString = "Horizontal_XBONE_P1";
                vertString = "Vertical_XBONE_P1";
                break;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        MoveVector = new Vector3(Input.GetAxis(horString), Input.GetAxis(vertString), 0.0f);
        MoveVector = (MoveVector * MoveSpeed) * Time.deltaTime;
        transform.Translate(MoveVector);
	}
}
