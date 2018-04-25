using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuDisable : MonoBehaviour {

    public GameObject Crosshair;

    private string submit;

    private void Start()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.PS4:
                submit = "Submit_PS4";
                break;

            case RuntimePlatform.XboxOne:
                submit = "Submit_XBONE";
                break;

            default:
                submit = "Submit_XBONE";
                break;
        }
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetButtonDown(submit))
        {
            Debug.Log("");
            Crosshair.SetActive(true);
            gameObject.SetActive(false);
        }
	}
}
