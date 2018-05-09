using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeInitialiser : MonoBehaviour
{
    int gameMode = 1;
    int playerCount = 1;


    private void Start()
    {
        gameMode = PlayerPrefs.GetInt("GamemodeKey");
        playerCount = PlayerPrefs.GetInt("PlayerCountKey");
        if (playerCount == 0)
        {
            playerCount = 1;
        }

        CreateGameMode();
    }


    void CreateGameMode()
    {
        GameMode mode = null;

        switch (gameMode)
        {
            case 0:
                mode = gameObject.AddComponent<FreeForAll_GameMode>();
                break;

            case 1:
                mode = gameObject.AddComponent<FreeForAll_GameMode>();
                break;

            case 2:
                mode = gameObject.AddComponent<GoldenGun_GameMode>();
                break;

            case 3:
                mode = gameObject.AddComponent<RocketsOnly_GameMode>();
                break;

        }

        if (mode != null)
        {
            mode.playerCount = playerCount;
        }
    }
}
