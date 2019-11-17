using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static int kills = 0;
    public static int headshots = 0;

    public static int hits = 0;
    public static int shots = 0;

    public static float levelTime = 0;
    public static bool timeStarted;

    public static int points = 0;
    public static int combo = 0;
    public static int highestCombo = 0;
    static float ctimer = 0;

    public int comboTimer = 3;
    public bool comboReset = true;
    public Text killsText;
    public Text shotsText;
    public Text timeText;
    public Text pointsText;
    public Text comboText;

    void Update()
    {
        if (timeStarted)
            levelTime += Time.deltaTime;

        if (combo != 0)
        {
            ctimer += Time.deltaTime;
            if (ctimer >= comboTimer)
            {
                if (comboReset)
                    combo = 0;
                else
                    combo = combo - 1 < 0 ? 0 : combo - 1;

                ctimer = 0;
            }
        }

        timeText.text = LevelTimeToString();
        killsText.text = kills + "\n" + headshots;
        shotsText.text = shots + "\n" + hits;
        pointsText.text = points.ToString("D5");

        comboText.text = combo <= 0 ? "" : "x" + combo;
        comboText.color = Color.Lerp(new Color(1f, 1f, 1f), new Color(1f, 0.45f, 0.45f), combo / 10f);
        comboText.fontSize = (int)Mathf.Lerp(28, 50, combo / 10f);
    }

    public static string LevelTimeToString()
    {
        return FloatTimeToString(levelTime);
    }

    public static string FloatTimeToString(float time)
    {
        System.TimeSpan t = System.TimeSpan.FromSeconds(time);
        return string.Format("{0:D2}:{1:D2}.{2:00}", t.Minutes, t.Seconds, t.Milliseconds / 10);
    }

    public static void Reset()
    {
        kills = 0;
        headshots = 0;
        hits = 0;
        shots = 0;
        levelTime = 0;
        points = 0;
        combo = 0;
        highestCombo = 0;
        ctimer = 0;
        timeStarted = false;
    }

    public static int AddPoints(int point)
    {
        int multiplier = combo <= 0 ? 1 : combo;
        int total = point * multiplier;

        points += total;
        combo++;
        ctimer = 0;
        highestCombo = combo > highestCombo ? combo : highestCombo;

        return total;
    }
}
