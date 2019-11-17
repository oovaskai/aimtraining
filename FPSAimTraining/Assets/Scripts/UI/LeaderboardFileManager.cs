using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;

public class LeaderboardFileManager
{
    string filepath;

    public LeaderboardFileManager(string filepath)
    {
        this.filepath = filepath;
    }

    public List<Record> ReadFile()
    {
        List<Record> records = new List<Record>();

        if (!File.Exists(filepath))
        {
            records.Add(new Record("ovasoft", 97.93886f, 423));
            return records;
        }

        XmlDocument doc = new XmlDocument();
        doc.Load(filepath);

        XmlNode recordsNode = doc.SelectSingleNode("records");
        XmlNodeList recordList = recordsNode.SelectNodes("record");

        foreach (XmlNode node in recordList)
        {
            Record rec = new Record(node.Attributes.GetNamedItem("name").Value, float.Parse(node.Attributes.GetNamedItem("time").Value), int.Parse(node.Attributes.GetNamedItem("points").Value));
            records.Add(rec);
        }

        records.Sort();
        return records;
    }

    public void WriteFile()
    {
        XmlDocument doc = new XmlDocument();
        doc.AppendChild(doc.CreateXmlDeclaration("1.0", "UTF-8", null));

        XmlNode records = doc.CreateElement("records");
        doc.AppendChild(records);

        foreach (Record record in Leaderboard.records)
        {
            XmlNode rec = doc.CreateElement("record");

            XmlAttribute name = doc.CreateAttribute("name");
            name.Value = record.name;
            rec.Attributes.Append(name);

            XmlAttribute time = doc.CreateAttribute("time");
            time.Value = record.time.ToString();
            rec.Attributes.Append(time);

            XmlAttribute points = doc.CreateAttribute("points");
            points.Value = record.points.ToString();
            rec.Attributes.Append(points);

            records.AppendChild(rec);
        }

        doc.Save(filepath);
    }
}
