using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayFinalScores : MonoBehaviour {

    int PlayerCount;

    int Player1Score = 10;
    int Player1Deaths;
    bool Player1Set = false;
    
    int Player2Score = 2;
    int Player2Deaths;
    bool Player2Set = false;

    int Player3Score = 10;
    int Player3Deaths;
    bool Player3Set = false;

    int Player4Score = 5;
    int Player4Deaths;
    bool Player4Set = false;

    public Text ScoreText;

    public List<int> scores = new List<int>();
    public List<int> positions = new List<int>();

    void GetScores()
    {
        PlayerCount = PlayerPrefs.GetInt("PlayerCountKey");
        Player1Score = PlayerPrefs.GetInt("P1_kills");
        scores.Add(Player1Score);
        Player1Deaths = PlayerPrefs.GetInt("P1_deaths");
        Player2Score = PlayerPrefs.GetInt("P2_kills");
        scores.Add(Player2Score);
        Player2Deaths = PlayerPrefs.GetInt("P2_deaths");
        Player3Score = PlayerPrefs.GetInt("P3_kills");
        scores.Add(Player3Score);
        Player3Deaths = PlayerPrefs.GetInt("P3_deaths");
        Player4Score = PlayerPrefs.GetInt("P4_kills");
        scores.Add(Player4Score);
        Player4Deaths = PlayerPrefs.GetInt("P4_deaths");
    }

    void SortScores()
    {
        scores.Sort();
        scores.Reverse();

        foreach (int i in scores)
        {
            if (i == Player1Score && !Player1Set)
            {
                positions.Add(1);
                Player1Set = true;
            }
            if (i == Player2Score && !Player2Set)
            {
                positions.Add(2);
                Player2Set = true;
            }
            if (i == Player3Score && !Player3Set)
            {
                positions.Add(3);
                Player3Set = true;
            }
            if (i == Player4Score && !Player4Set)
            {
                positions.Add(4);
                Player4Set = true;
            }
        }

    }

    

    void DisplayScore()
    {
        ScoreText.text = "Player " + positions[0] + ": " + scores[0] +
                         "\nPlayer " + positions[1] + ": " + scores[1] +
                         "\nPlayer " + positions[2] + ": " + scores[2] +
                         "\nPlayer " + positions[3] + ": " + scores[3];
    }

	// Use this for initialization
	void Start () {
        GetScores();
        SortScores();
        DisplayScore();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
