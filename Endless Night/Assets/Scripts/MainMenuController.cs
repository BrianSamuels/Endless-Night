using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public AudioSource audioData;

    void Start()
    {
        audioData = GetComponent<AudioSource>();
        audioData.playOnAwake = true;
        audioData.loop = true;
        //DontDestroyOnLoad(audioData);
    }
    public void play()
    {
        SceneManager.LoadScene("Level1");
    }
    public void play2()
    {
        SceneManager.LoadScene("Level2");
    }

    public void play3()
    {
        SceneManager.LoadScene("Level3");
    }
    public void levelSelector()
    {
        SceneManager.LoadScene("LevelSelector");
    }
    public void goBack()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void winLoseCondition()
    {
        SceneManager.LoadScene("menuWinLose");
    }

    public void controlsMenu()
    {
        SceneManager.LoadScene("ControlsMenu");
    }
    public void storyMenu()
    {
        SceneManager.LoadScene("StoryMenu");
    }

    public void mute()
    {
        if (audioData.mute == false)
        {
            audioData.mute = true;
        }
        else if(audioData.mute == true)
        {
            audioData.mute = false;
        }
    }
}
