using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject[] content;

    AudioSource sound;

    private void Start()
    {
        sound = GetComponent<AudioSource>();

        Leaderboard.LoadTimes();

        Settings.MouseSensitivity = PlayerPrefs.GetFloat("Sensitivity");
        Settings.AimMultiplier = PlayerPrefs.GetFloat("AimMultiplier");
        Settings.InvertMouse = bool.Parse(PlayerPrefs.GetString("InvertMouse"));

        Time.timeScale = 1;
    }

    public void LoadLevel(string levelName)
    {
        sound.Play();
        new WaitForSeconds(5);
        SceneManager.LoadScene(levelName);
    }

    public void QuitGame()
    {
        sound.Play();
        Debug.Log("Quitting game");
        Application.Quit();
    }

    public void OpenContent(string name)
    {
        foreach (GameObject page in content)
        {
            if (page.name == name)
                page.SetActive(true);

            else
                page.SetActive(false);
        }
        sound.Play();
    }
}
