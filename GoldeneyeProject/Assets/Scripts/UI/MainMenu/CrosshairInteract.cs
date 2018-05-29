using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CrosshairInteract : MonoBehaviour {

    public Sprite normal;
    public Sprite highlight;
    int CurrentButton;
    public int playerNumber = 2;
    public int levelNumber = 1;
    public string levelKey = "Archives";
    public string levelName = "Archives(Ciaran)";
    public int gamemode = 1;
    public string gamemodeName = "Free-for-all";
    public GameObject MenuOverlay;
    public GameObject Attributes;
    Text AttrText;

    private string submit;

	// Use this for initialization
	void Start ()
    {
        AttrText = Attributes.GetComponent<Text>();
        
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

    void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Image>().sprite = highlight;
    }

    void ActivateButton(int bNumber)
    {
        switch (bNumber)
        {
            case 1:
                if (playerNumber < 4)
                {
                    playerNumber++;
                }
                else
                {
                    playerNumber = 2;
                }
                UpdateAttributes();
                break;
            case 2:
                PickLevel();
                UpdateAttributes();
                break;
            case 3:
                PickGamemode();
                UpdateAttributes();
                break;
            case 4:
                StartGame();
                break;
            case 5:
                Back();
                break;
            default:
                break;
        }

        UpdateAttributes();
    }

    void PickLevel()
    {
        switch (levelNumber)
        {
            case 1:
                levelName = "Temple(Josh)";
                levelKey = "Temple_Josh";
                levelNumber++;
                break;
            case 2:
                levelName = "Complex(Matt)";
                levelKey = "Complex";
                levelNumber++;
                break;
            case 3:
                levelName = "Library(Mitch)";
                levelKey = "Library_Mitch";
                levelNumber++;
                break;
            case 4:
                levelName = "Basement(Harry)";
                levelKey = "Basement_Harry";
                levelNumber++;
                break;
            case 5:
                levelName = "Archives(Ciaran)";
                levelKey = "Archives";
                levelNumber = 1;
                break;
            default:
                levelName = "Archives(Ciaran)";
                levelKey = "Archives";
                levelNumber++;
                break;
        }
    }

    void PickGamemode()
    {
        switch (gamemode)
        {
            case 1:
                gamemodeName = "The man with the golden gun";
                gamemode++;
                break;
            case 2:
                gamemodeName = "Rowdy Rocket Rumble";
                gamemode++;
                break;
            case 3:
                gamemodeName = "Free-for-all";
                gamemode = 1;
                break;
            default:
                gamemodeName = "Free-for-all";
                gamemode = 0;
                break;
        }
    }

    void StartGame()
    {
        PlayerPrefs.SetInt("GamemodeKey", gamemode);
        PlayerPrefs.SetInt("PlayerCountKey", playerNumber);
        PlayerPrefs.SetString("LevelKey", levelKey);
        //Add Scene Transition Here

        SceneManager.LoadScene(levelKey);
    }

    void Back()
    {
        SceneManager.LoadScene(0);
    }

    void UpdateAttributes()
    {
        AttrText.text = playerNumber + "\n\n" + levelName + "\n\n" + gamemodeName;
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetButtonDown(submit))
        {
            CurrentButton = other.gameObject.GetComponent<MenuButton>().ButtonNumber;
            ActivateButton(CurrentButton);
        }
    }

    void OnTriggerExit(Collider other)
    {
        other.GetComponent<Image>().sprite = normal;
        CurrentButton = 0;
    }
}
