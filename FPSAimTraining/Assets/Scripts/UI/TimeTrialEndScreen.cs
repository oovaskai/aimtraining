using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeTrialEndScreen : MonoBehaviour
{
    public Text stats;

    GameObject buttons;
    GameObject newRecord;

    public Text inputText;
    public Button okButton;

    InputField inputField;

    float endTime = 0;
    int timeBonus = 0;
    int totalPoints = 0;

    // Start is called before the first frame update
    void Start()
    {
        buttons = transform.Find("Buttons").gameObject;
        newRecord = transform.Find("NewRecord").gameObject;

        inputField = newRecord.GetComponentInChildren<InputField>();

        endTime = PlayerStats.levelTime;

        float bonus = TimeTrial.targetTime - endTime;
        timeBonus = bonus > 0 ? (int)(PlayerStats.points * bonus/100) : 0;
        totalPoints = PlayerStats.points + timeBonus;

        stats.text = StatsToString();

        if (Leaderboard.IsRecord(endTime))
        {
            buttons.SetActive(false);
            newRecord.SetActive(true);
            inputField.Select();
        }
    }

    void Update()
    {
        if (inputField.isFocused && Input.GetKeyDown(KeyCode.Return))
            NewRecord();
    }

    public void NewRecord()
    {
        if (inputText.text == "")
        {
            inputField.Select();
            return;
        }

        Leaderboard.NewTime(inputText.text, endTime, totalPoints);
        newRecord.SetActive(false);
        buttons.SetActive(true);
    }

    public void PlayerNameChanged()
    {
        okButton.interactable = inputText.text == "" ? false : true;
    }

    public void Cancel()
    {
        newRecord.SetActive(false);
        buttons.SetActive(true);
    }

    string StatsToString()
    {
        float ratio = PlayerStats.kills == 0 ? 0 : (float)PlayerStats.headshots / PlayerStats.kills;
        float accuracy = PlayerStats.shots == 0 ? 0 : (float)PlayerStats.hits / PlayerStats.shots;

        return PlayerStats.FloatTimeToString(endTime) + "\n" +
                "\n" +
                PlayerStats.points + "\n" +
                "+" + timeBonus + "\n" +
                totalPoints + "\n" +
                "\n" +
                "x" + PlayerStats.highestCombo + "\n" +
                "\n" +
                PlayerStats.kills + "\n" +
                PlayerStats.headshots + "\n" +
                string.Format("{0:F2} %", 100 * ratio) + "\n" +
                "\n" +
                PlayerStats.shots + "\n" +
                PlayerStats.hits + "\n" +
                string.Format("{0:F2} %", 100 * accuracy);
    }
}
