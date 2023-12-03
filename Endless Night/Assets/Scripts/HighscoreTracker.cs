using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighscoreTracker : MonoBehaviour
{
    public float highscoreLv1 = 0;
    public float hsKillsLv1 = 0;
    public float hsDistanceLv1 = 0;

    public float highscoreLv2 = 0;
    public float hsKillsLv2 = 0;
    public float hsDistanceLv2 = 0;

    public float highscoreLv3 = 0;
    public float hsKillsLv3 = 0;
    public float hsDistanceLv3 = 0;

    public float score1 = 0;
    public float score2 = 0;
    public float score3 = 0;

    public float newKill;
    public float newDistance;
    
    private void Awake()
    {
        GameObject[] trackerObj = GameObject.FindGameObjectsWithTag("highscore");
        if (trackerObj.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level1"))
        {
            score1 = newDistance * newKill;
        }
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level2"))
        {
            score2 = newDistance * newKill;
        }
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level3"))
        {
            score3 = newDistance * newKill;
        }
        //Updates highscore for all three levels
        //score = newDistance * newKill;
        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level1") && highscoreLv1 < score1)
        {
            highscoreLv1 = score1;
            hsDistanceLv1 = newDistance;
            hsKillsLv1 = newKill;
        }
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level2") && highscoreLv2 < score2)
        {
            highscoreLv2 = score2;
            hsDistanceLv2 = newDistance;
            hsKillsLv2 = newKill;
        }
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level3") && highscoreLv3 < score3)
        {
            highscoreLv3 = score3;
            hsDistanceLv3 = newDistance;
            hsKillsLv3 = newKill;
        }
    }
}
