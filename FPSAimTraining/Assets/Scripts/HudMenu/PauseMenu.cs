using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public static bool paused = false;
    public static bool allowESC = true;
    public GameObject menu;
    public bool startPaused = false;

    private void Awake()
    {
        if (startPaused)
        {
            TogglePauseMenu();
        }
        else
            LockCursor();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && allowESC)
        {
            TogglePauseMenu();
        }
    }

    public void TogglePauseMenu()
    {
        menu.SetActive(!paused);

        if (!paused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    public static void PauseGame()
    {
        Time.timeScale = 0;
        paused = true;
        UnlockCursor();
    }

    public static void ResumeGame()
    {
        Time.timeScale = 1;
        paused = false;
        LockCursor();
    }

    public static void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public static void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RetryLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }
}
