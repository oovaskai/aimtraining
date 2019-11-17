using System;

public class Record : IComparable<Record>
{
    public string name;
    public float time;
    public int points;
    public bool isNew;

    public Record(string name, float time, int points)
    {
        this.name = name;
        this.time = time;
        this.points = points;
        isNew = false;
    }

    public int CompareTo(Record other)
    {
        if (other.time == time)
            return 0;

        if(other.time < time)
            return 1;
        else return 0;
    }
}
