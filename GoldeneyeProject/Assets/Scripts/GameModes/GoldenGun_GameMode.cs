using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenGun_GameMode : GameMode
{
    [SerializeField] protected bool useSpecialKills = true;
    [SerializeField] protected int specialKillsToWin = 10;

    GoldenGun_GameMode()
    {
        usesTimeLimit = false;
        timerMins = 4;
        timerSeconds = 59;

        useKillsToWin = true;
        killsToWin = 20;
    }


    public override void PlayerKilled(int killerID, bool special = false)
    {
        Debug.Log("Hit");
        if (special)
        {
            switch (useSpecialKills)
            {
                case true:
                    if (leaderboard[killerID].IncreaseSpecialKill() >= specialKillsToWin)
                    {
                        EndGame();
                    }
                    break;

                case false:
                    leaderboard[killerID].IncreaseSpecialKill();
                    break;
            }

            Debug.Log("Player " + (killerID + 1) + " has " + leaderboard[killerID].specialKills + " special kills");
        }
        else
        {
            base.PlayerKilled(killerID);
        }
    }
}
