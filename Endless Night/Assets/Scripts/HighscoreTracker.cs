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

    public float newKill1;
    public float newDistance1;

    public float newKill2;
    public float newDistance2;

    public float newKill3;
    public float newDistance3;

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
            score1 = newDistance1 * newKill1;
        }
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level2"))
        {
            score2 = newDistance2 * newKill2;
        }
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level3"))
        {
            score3 = newDistance3 * newKill3;
        }
        //Updates highscore for all three levels
        //score = newDistance * newKill;
        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level1") && highscoreLv1 < score1)
        {
            highscoreLv1 = score1;
            hsDistanceLv1 = newDistance1;
            hsKillsLv1 = newKill1;
        }
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level2") && highscoreLv2 < score2)
        {
            highscoreLv2 = score2;
            hsDistanceLv2 = newDistance2;
            hsKillsLv2 = newKill2;
        }
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level3") && highscoreLv3 < score3)
        {
            highscoreLv3 = score3;
            hsDistanceLv3 = newDistance3;
            hsKillsLv3 = newKill3;
        }
    }
}
