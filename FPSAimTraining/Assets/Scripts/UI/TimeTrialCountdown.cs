using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeTrialCountdown : MonoBehaviour
{
    public int count;

    public GameObject startScreen;

    bool countdown = false;

    public void StartCountdown()
    {
        startScreen.SetActive(false);
        gameObject.SetActive(true);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        StartCoroutine("Countdown");
    }

    IEnumerator Countdown()
    {
        countdown = true;
        while (countdown)
        {
            GetComponent<Text>().text = count--.ToString();
            GetComponent<AudioSource>().PlayOneShot(GetComponent<SoundManager>().audioClips[0]);

            if (count == 0)
            {
                countdown = false;
            }

            yield return new WaitForSecondsRealtime(1);

        }
        GetComponent<AudioSource>().PlayOneShot(GetComponent<SoundManager>().audioClips[1]);
        TimeTrial.StartTrial();
    }
}
