using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    private void Awake()
    {
        //Continues to play audiosource object even when moving to another scene
        GameObject[] musicObj = GameObject.FindGameObjectsWithTag("bgMusic");
        if(musicObj.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
