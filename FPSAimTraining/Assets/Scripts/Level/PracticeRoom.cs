using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticeRoom : MonoBehaviour
{
    void Start()
    {
        PlayerStats.Reset();
        PlayerStats.timeStarted = true;
        PauseMenu.ResumeGame();
    }
}
