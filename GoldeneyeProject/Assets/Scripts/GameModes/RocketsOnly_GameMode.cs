﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketsOnly_GameMode : GameMode
{
    RocketsOnly_GameMode()
    {
        usesTimeLimit = true;
        timerMins = 4;
        timerSeconds = 59;

        useKillsToWin = true;
        killsToWin = 20;
    }

    protected override void StartGame()
    {
        spawner.SpawnWeaponOfType(WeaponType.ROCKET_LAUNCHER);
        base.StartGame();
    }
}
