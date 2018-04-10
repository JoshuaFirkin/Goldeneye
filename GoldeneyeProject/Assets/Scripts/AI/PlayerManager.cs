using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    //  singleton automatically creates static initialization method (singletons can also inherit from base classes which static classes cant)
    #region Singleton
    public static PlayerManager instance;

    void Awake() {
        instance = this;
    }
    #endregion

    public GameObject player;

}
