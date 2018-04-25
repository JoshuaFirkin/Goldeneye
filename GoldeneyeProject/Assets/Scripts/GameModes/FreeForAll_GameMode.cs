using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeForAll_GameMode : GameMode
{
    FreeForAll_GameMode()
    {
        usesTimeLimit = true;
        timerMins = 4;
        timerSeconds = 59;

        useKillsToWin = true;
        killsToWin = 20;
    }
}
