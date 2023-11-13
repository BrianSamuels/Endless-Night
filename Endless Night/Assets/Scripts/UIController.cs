using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    
    Player player;
    //TMP = text mesh pro
    TMP_Text distanceText;
    TMP_Text killCountText;

    public GameObject results;
    TMP_Text finalDistanceText;
    TMP_Text finalKillCountText;
    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        distanceText = GameObject.Find("DistanceText").GetComponent<TMP_Text>();
        killCountText = GameObject.Find("KillCountText").GetComponent<TMP_Text>();

        finalDistanceText = GameObject.Find("FinalDistance").GetComponent<TMP_Text>();
        finalKillCountText = GameObject.Find("FinalKillCount").GetComponent<TMP_Text>();

        results.SetActive(false);
    }   

    // Update is called once per frame
    void Update()
    {
        //Record and display player progression
        int distance = Mathf.FloorToInt(player.distance);
        distanceText.text = distance + " m";

        int kills = Mathf.FloorToInt(player.killCount);
        killCountText.text = kills + " kills";

        if (player.isDead)
        {
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
        SceneManager.LoadScene("SampleScene");
    }

    public void Retry2()
    {
        SceneManager.LoadScene("SampleScene 1");
    }
}
