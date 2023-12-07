using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerSpawner : MonoBehaviour
{
    //public GameObject player;
    public Vector3 pos = new Vector3(7.9f, 9.6f, 0f);
    private void Awake()
    {
        //Instantiate(player, new Vector3(7.9f, 9.6f, 0f),Quaternion.identity);
        //player.transform.position = new Vector3(7.9f, 9.6f, 0f);
       /*
        GameObject[] positionObj = GameObject.FindGameObjectsWithTag("Respawn");
        if (positionObj.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
       */
        GameObject.FindGameObjectWithTag("Player").transform.position = pos;
       
    }
    // Start is called before the first frame update
    void Start()
    {
        /*
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level1"))
        {
            player.transform.position = new Vector3(7.9f, 9.6f, 0f);
        }
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level2"))
        {
            player.transform.position = new Vector3(7.9f, 9.6f, 0f);
        }
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level3"))
        {
            player.transform.position = new Vector3(7.9f, 9.6f, 0f);
        }
        //player.transform.position = new Vector3(7.9f, 9.6f, 0f);
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
