using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    public List<GameObject> players = new List<GameObject>();

    [SerializeField]
    private int timerMins = 4;
    [SerializeField]
    private int timerSeconds = 59;

	void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}

    void EndGame()
    {
        foreach (GameObject player in players)
        {
            PlayerController controller = player.GetComponent<PlayerController>();
            controller.DisableInput();
        }
    }
}
