using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMode : MonoBehaviour
{
    #region Singleton
    public static GameMode instance;
    protected void Awake()
    {
        instance = this;
    }
    #endregion

    public class PlayerLeaderboard
    {
        public GameObject player { get; private set; }
        public PlayerController controller { get; private set; }
        public PlayerHealth health { get; private set; }
        public PlayerInventory inventory { get; private set; }
        public int playerID { get; private set; }
        public int kills { get; private set; }
        public int specialKills { get; private set; }
        public int deaths { get; private set; }

        public PlayerLeaderboard(GameObject _player, int _id)
        {
            player = _player;
            playerID = _id;

            controller = player.GetComponent<PlayerController>();
            health = player.GetComponent<PlayerHealth>();
            inventory = player.GetComponent<PlayerInventory>();

            kills = 0;
            specialKills = 0;
            deaths = 0;
        }

        public int IncreaseKill()
        {
            return kills++;
        }

        public int IncreaseSpecialKill()
        {
            return specialKills++;
        }

        public int IncreaseDeath()
        {
            return deaths++;
        }
    }

    public int playerCount;
    public List<GameObject> players = new List<GameObject>();
    public List<PlayerLeaderboard> leaderboard = new List<PlayerLeaderboard>();

    [SerializeField] protected bool usesTimeLimit = true;
    [SerializeField] protected int timerMins = 4;
    [SerializeField] protected int timerSeconds = 59;

    [SerializeField] protected bool useKillsToWin = true;
    [SerializeField] protected int killsToWin = 10;

    protected SpawnPlayer spawner;
    protected TimeKeeper timeKeeper;

    protected int killLead = 0;
    protected bool gameOver = false;

	protected virtual void Start ()
    {
        spawner = GetComponent<SpawnPlayer>();
        timeKeeper = GetComponent<TimeKeeper>();
        timeKeeper.DisplayTime(timerMins, timerSeconds);
        players = spawner.InstancePlayer(playerCount);

        for (int i = 0; i < players.Count; i++)
        {
            int id = i + 1;

            leaderboard.Add(new PlayerLeaderboard(players[i], id));
            players[i].GetComponent<PlayerController>().SetID(i);
            Debug.Log(leaderboard[i].playerID);
        }

        StartGame();
	}
	

    protected virtual void StartGame()
    {
        spawner.SpawnWeaponsNormally();
        StartCoroutine(TimerCountdown());
    }


    protected virtual IEnumerator TimerCountdown()
    {
        while (!gameOver)
        {
            if (timerMins == 0 && timerSeconds == 0)
            {
                EndGame();
                yield return 0;
            }

            if (timerSeconds <= 0)
            {
                yield return new WaitForSeconds(1);
                timerMins--;
                timerSeconds = 59;
            }

            yield return new WaitForSeconds(1);
            timerSeconds--;

            if (timeKeeper != null)
            {
                timeKeeper.DisplayTime(timerMins, timerSeconds);
            }
        }
    }


    public virtual void PlayerKilled(int killerID, bool special = false)
    {
        switch (useKillsToWin)
        {
            case true:
                if (leaderboard[killerID].IncreaseKill() >= killsToWin)
                {
                    EndGame();
                }
                break;

            case false:
                leaderboard[killerID].IncreaseKill();
                break;
        }

        Debug.Log("Player " + (killerID + 1) + " has " + leaderboard[killerID].kills + " kills");
    }


    public virtual void PlayerDeath(int deathID)
    {
        leaderboard[deathID].IncreaseDeath();
        leaderboard[deathID].health.Respawn();
        spawner.MovePlayerToPoint(leaderboard[deathID].player.transform);
    }


    public PlayerLeaderboard FindPlayerByID(int id)
    {
        foreach (PlayerLeaderboard pLeaderboard in leaderboard)
        {
            if (pLeaderboard.playerID == id)
            {
                return pLeaderboard;
            }
        }

        return null;
    }


    protected virtual void EndGame()
    {
        Debug.Log("GAME OVER");
        gameOver = true;

        foreach (GameObject playerObj in players)
        {
            PlayerController controller = playerObj.GetComponent<PlayerController>();
            controller.DisableInput();
        }

        int mostKills = 0;
        PlayerLeaderboard winningPlayer = null;
        foreach (PlayerLeaderboard playerLeaderboard in leaderboard)
        {
            if (playerLeaderboard.kills > mostKills)
            {
                mostKills = playerLeaderboard.kills;
                winningPlayer = playerLeaderboard;
            }
        }

        SaveScores();
        StartCoroutine(SceneTransition());
    }

    protected virtual IEnumerator SceneTransition()
    {
        timeKeeper.DisplayEndGame();
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Scoreboard");
    }

    public void SpawnGoldenGun()
    {
        spawner.SpawnGoldenGun();
        return;
    }

    void SaveScores()
    {
        //PlayerPrefs.SetInt("winner", winnerID);

        for (int i = 0; i < leaderboard.Count; i++)
        {
            string id = "P" + leaderboard[i].playerID.ToString() + "_";

            PlayerPrefs.SetInt(id + "kills", leaderboard[i].kills);
            PlayerPrefs.SetInt(id + "deaths", leaderboard[i].deaths);
            PlayerPrefs.SetInt(id + "specials", leaderboard[i].specialKills);
        }
    }
}
