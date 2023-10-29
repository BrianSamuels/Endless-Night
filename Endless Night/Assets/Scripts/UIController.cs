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

    public GameObject results;
    TMP_Text finalDistanceText;
    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        distanceText = GameObject.Find("DistanceText").GetComponent<TMP_Text>();

        finalDistanceText = GameObject.Find("FinalDistance").GetComponent<TMP_Text>();

        //results = GameObject.Find("Results").GetComponent<GameObject>();
        results.SetActive(false);
    }   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int distance = Mathf.FloorToInt(player.distance);
        distanceText.text = distance + " m";

        if (player.isDead)
        {
            results.SetActive(true);
            finalDistanceText.text = distance + " m";
        }
    }

    public void Quit()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Retry()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
