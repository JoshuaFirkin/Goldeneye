using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        public int playerID { get; private set; }
        public int kills { get; private set; }
        public int deaths { get; private set; }

        public PlayerLeaderboard(GameObject _player, int _id)
        {
            player = _player;
            playerID = _id;

            kills = 0;
            deaths = 0;
        }

        public int IncreaseKill()
        {
            return kills++;
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

    protected int killLead = 0;
    protected bool gameOver = false;

	protected void Start ()
    {
        spawner = GetComponent<SpawnPlayer>();
        players = spawner.InstancePlayer(playerCount);

        for (int i = 0; i < players.Count; i++)
        {
            leaderboard.Add(new PlayerLeaderboard(players[i], i + 1));
            Debug.Log(leaderboard[i].playerID);
        }

        StartCoroutine(TimerCountdown());
	}
	

    protected void StartGame()
    {
        StartCoroutine(TimerCountdown());
    }


    protected IEnumerator TimerCountdown()
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

            // @TODO: ADD TIMER UI HERE.
        }
    }

    public void PlayerKilled(int killerID)
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
    }


    public void PlayerDeath(int deathID)
    {
        leaderboard[deathID].IncreaseDeath();
    }


    protected void EndGame()
    {
        Debug.Log("GAME OVER");
        gameOver = true;

        foreach (GameObject player in players)
        {
            PlayerController controller = player.GetComponent<PlayerController>();
            controller.DisableInput();
        }

        // @TODO: Add player info here.
    }
}
