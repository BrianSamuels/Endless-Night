using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    
    Player player;
    public GameObject scoreTracker;
    private HighscoreTracker highscoreChecker;
    TMP_Text distanceText;
    TMP_Text killCountText;
    TMP_Text lifeCountText;

    public GameObject results;
    public GameObject newHighscore;
    TMP_Text finalDistanceText;
    TMP_Text finalKillCountText;
    private void Awake()
    {
        scoreTracker = GameObject.FindWithTag("highscore");
        highscoreChecker = scoreTracker.GetComponent<HighscoreTracker>();

        player = GameObject.Find("Player").GetComponent<Player>();
        distanceText = GameObject.Find("DistanceText").GetComponent<TMP_Text>();
        killCountText = GameObject.Find("KillCountText").GetComponent<TMP_Text>();
        lifeCountText = GameObject.Find("LivesText").GetComponent<TMP_Text>();

        finalDistanceText = GameObject.Find("FinalDistance").GetComponent<TMP_Text>();
        finalKillCountText = GameObject.Find("FinalKillCount").GetComponent<TMP_Text>();

        results.SetActive(false);
        newHighscore.SetActive(false);
    }   

    // Update is called once per frame
    void Update()
    {
        //Record and display player progression
        int distance = Mathf.FloorToInt(player.distance);
        distanceText.text = distance + " m";
        
        int kills = Mathf.FloorToInt(player.killCount);
        killCountText.text = kills + " kills";

       
        //Alert player if life is low
        int lives = Mathf.FloorToInt(player.lives);
        if (lives <= 5)
        {
            lifeCountText.color = Color.red;
        }
        lifeCountText.text = lives + " lives";

        //Display results upon player death
        if (player.isDead)
        {
            //Updates highscore if necessary and notifies player if they got a new highscore
            if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level1"))
            {
                if (distance * kills > highscoreChecker.highscoreLv1)
                {
                    newHighscore.SetActive(true);
                }
                highscoreChecker.newDistance1 = distance;
                highscoreChecker.newKill1 = kills;
            }
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level2"))
            {
                if (distance * kills > highscoreChecker.highscoreLv2)
                {
                    newHighscore.SetActive(true);
                }
                highscoreChecker.newDistance2 = distance;
                highscoreChecker.newKill2 = kills;
            }
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level3"))
            {
                if (distance * kills > highscoreChecker.highscoreLv3)
                {
                    newHighscore.SetActive(true);
                }
                highscoreChecker.newDistance3 = distance;
                highscoreChecker.newKill3 = kills;
            }
            //Alerts player if they acheived a new highscore
            /*
            if (distance * kills > highscoreChecker.highscoreLv1 && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level1"))
            {
                newHighscore.SetActive(true);
            }
            if (distance * kills > highscoreChecker.highscoreLv2 && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level2"))
            {
                newHighscore.SetActive(true);
            }
            if (distance * kills > highscoreChecker.highscoreLv3 && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level3"))
            {
                newHighscore.SetActive(true);
            }
            //Final Output after player death
            highscoreChecker.newDistance = distance;
            highscoreChecker.newKill = kills;
            */
            //Activate results upon player death
            results.SetActive(true);
            finalDistanceText.text = distance + " m";
            finalKillCountText.text = kills + " kills";
        }
    }


    //Scene controller
    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Retry()
    {
        SceneManager.LoadScene("Level1");
    }

    public void Retry2()
    {
        SceneManager.LoadScene("Level2");
    }

    public void Retry3()
    {
        SceneManager.LoadScene("Level3");
    }
}
