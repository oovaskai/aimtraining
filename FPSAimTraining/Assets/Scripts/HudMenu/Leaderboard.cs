using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    public static List<Record> records = new List<Record>();

    static LeaderboardFileManager file;
    static bool loaded;


    public GameObject boardRow;

    public void Start()
    {
        records.Sort();

        for (int i = 0; i < records.Count; i++)
        {
            GameObject row = Instantiate(boardRow, transform);
            row.transform.localPosition = new Vector3(row.transform.localPosition.x, row.transform.localPosition.y - i*22 - 5, 0);
            row.transform.Find("Position").GetComponent<Text>().text = i + 1 + ".";
            row.transform.Find("Time").GetComponent<Text>().text = PlayerStats.FloatTimeToString(records[i].time);
            row.transform.Find("Points").GetComponent<Text>().text = records[i].points.ToString();

            Text nameText = row.transform.Find("Name").GetComponent<Text>();
            nameText.text = records[i].name;
            if (records[i].isNew)
                nameText.color = new Color(1f, 0.45f, 0.45f);
        }
    }

    public static void NewTime(string name, float time, int points)
    {
        bool added = false;

        foreach (Record record in records)
        {
            record.isNew = false;
        }

        Record rec = new Record(name, time, points);
        rec.isNew = true;

        for (int i = 0; i < records.Count && !added; i++)
        {
            if (time < records[i].time)
            {
                records.Insert(i, rec);

                if (records.Count > 10)
                    records.RemoveAt(10);

                added = true;
            }
        }

        if (records.Count < 10 && !added)
        {
            records.Add(rec);
        }

        records.Sort();
        file.WriteFile();
    }

    public static bool IsRecord(float time)
    {
        if (records.Count < 10)
        {
            return true;
        }

        foreach (Record record in records)
        {
            if (time < record.time)
                return true;
        }

        return false;
    }

    public static void LoadTimes()
    {
        if (!loaded)
        {
            file = new LeaderboardFileManager(Application.dataPath + "\\records.xml");
            records = file.ReadFile();

            loaded = true;
        }
    }
}
