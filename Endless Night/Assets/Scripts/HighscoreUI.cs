using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HighscoreUI : MonoBehaviour
{
    public GameObject testHighscore;
    private HighscoreTracker currentHighscore;

    //Highscore: Level 1 
    TMP_Text lv1HighscoreText;
    TMP_Text lv1DistanceText;
    TMP_Text lv1KillsText;

    //Highscore: Level 2
    TMP_Text lv2HighscoreText;
    TMP_Text lv2DistanceText;
    TMP_Text lv2KillsText;

    //Highscore: Level 3
    TMP_Text lv3HighscoreText;
    TMP_Text lv3DistanceText;
    TMP_Text lv3KillsText;

    private void Awake()
    {
        testHighscore = GameObject.FindWithTag("highscore");
        currentHighscore = testHighscore.GetComponent<HighscoreTracker>();

        //Highscore: Level 1 
        lv1HighscoreText = GameObject.Find("Lv1HighscoreText").GetComponent<TMP_Text>();
        lv1DistanceText = GameObject.Find("Lv1DistanceText").GetComponent<TMP_Text>();
        lv1KillsText = GameObject.Find("Lv1KillsText").GetComponent<TMP_Text>();

        //Highscore: Level 2
        lv2HighscoreText = GameObject.Find("Lv2HighscoreText").GetComponent<TMP_Text>();
        lv2DistanceText = GameObject.Find("Lv2DistanceText").GetComponent<TMP_Text>();
        lv2KillsText = GameObject.Find("Lv2KillsText").GetComponent<TMP_Text>();

        //Highscore: Level 3
        lv3HighscoreText = GameObject.Find("Lv3HighscoreText").GetComponent<TMP_Text>();
        lv3DistanceText = GameObject.Find("Lv3DistanceText").GetComponent<TMP_Text>();
        lv3KillsText = GameObject.Find("Lv3KillsText").GetComponent<TMP_Text>();
    }

    //Output display
    // Update is called once per frame
    void Update()
    {
        //Highscore: Level 1 display output
        float lv1Highscore = currentHighscore.highscoreLv1;
        float lv1Kills = currentHighscore.hsKillsLv1;
        float lv1Distance = currentHighscore.hsDistanceLv1;
        lv1HighscoreText.text = "HighScore: " + lv1Highscore;
        lv1DistanceText.text = "Distance Traveled: " + lv1Distance + " m";
        lv1KillsText.text = "Enemies Slain: " + lv1Kills;

        //Highscore: Level 2 display output
        float lv2Highscore = currentHighscore.highscoreLv2;
        float lv2Kills = currentHighscore.hsKillsLv2;
        float lv2Distance = currentHighscore.hsDistanceLv2;
        lv2HighscoreText.text = "HighScore: " + lv2Highscore;
        lv2DistanceText.text = "Distance Traveled: " + lv2Distance + " m";
        lv2KillsText.text = "Enemies Slain: " + lv2Kills;

        //Highscore: Level 3 display output
        float lv3Highscore = currentHighscore.highscoreLv3;
        float lv3Kills = currentHighscore.hsKillsLv3;
        float lv3Distance = currentHighscore.hsDistanceLv3;
        lv3HighscoreText.text = "HighScore: " + lv3Highscore;
        lv3DistanceText.text = "Distance Traveled: " + lv3Distance + " m";
        lv3KillsText.text = "Enemies Slain: " + lv3Kills;
    }
}
