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
    }
    public void play()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void play2()
    {
        SceneManager.LoadScene("SampleScene 1");
    }
    public void levelSelector()
    {
        SceneManager.LoadScene("LevelSelector");
    }
    public void goBack()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void controlsMenu()
    {
        SceneManager.LoadScene("ControlsMenu");
    }
    public void storyMenu()
    {
        SceneManager.LoadScene("StoryMenu");
    }
}
