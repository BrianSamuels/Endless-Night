using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private AudioSource audioData;
    public GameObject objectMusic;

    void Start()
    {
        objectMusic = GameObject.FindWithTag("bgMusic");
        audioData = objectMusic.GetComponent<AudioSource>();
        // audioData = GetComponent<AudioSource>();
        //audioData.mute = false;

        if (!audioData.isPlaying)
        {
            audioData.Play();
        }
        audioData.playOnAwake = true;
        audioData.loop = true;
        
    }
    public void play()
    {
        SceneManager.LoadScene("Level1");
        audioData.Stop();
        //audioData.mute = true;
    }
    public void play2()
    {
        SceneManager.LoadScene("Level2");
        audioData.Stop();
    }

    public void play3()
    {
        SceneManager.LoadScene("Level3");
        audioData.Stop();
    }
    public void levelSelector()
    {
        SceneManager.LoadScene("LevelSelector");
    }
    public void goBack()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void howToPlayMenu()
    {
        SceneManager.LoadScene("HowToPlay");
    }

    public void controlsMenu()
    {
        SceneManager.LoadScene("ControlsMenu");
    }
    public void storyMenu()
    {
        SceneManager.LoadScene("StoryMenu");
    }
    public void highscoreMenu()
    {
        SceneManager.LoadScene("HighScore");
    }

    //Mute Button
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
