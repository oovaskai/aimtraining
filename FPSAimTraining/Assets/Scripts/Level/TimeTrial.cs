using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeTrial : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject endScreen;
    public float targetLevelTime;

    public static float targetTime;

    static CrosshairController crosshair;
    static Canvas canvas;
    static Transform startScreenCopy;
    static GameObject endScreenCopy;
    static AudioSource sound;

    void Start()
    {
        crosshair = FindObjectOfType<CrosshairController>();
        crosshair.crosshairEnabled = false;

        PlayerStats.Reset();
        PauseMenu.PauseGame();
        PauseMenu.allowESC = false;

        targetTime = targetLevelTime;

        canvas = FindObjectOfType<Canvas>();
        
        startScreenCopy = Instantiate(startScreen, canvas.transform).transform;
        startScreenCopy.transform.Find("StartScreen").Find("TargetTime").GetComponent<Text>().text = "Target time: " + PlayerStats.FloatTimeToString(targetLevelTime);
        endScreenCopy = endScreen;
        sound = GetComponent<AudioSource>();
    }


    public static void StartTrial()
    {
        crosshair.crosshairEnabled = true;
        PlayerStats.timeStarted = true;
        PauseMenu.ResumeGame();
        PauseMenu.allowESC = true;
        Destroy(startScreenCopy.gameObject);
        sound.Play();
    }

    public static void EndTrial()
    {
        PauseMenu.PauseGame();
        PauseMenu.allowESC = false;
        Instantiate(endScreenCopy, canvas.transform);
    }
}
