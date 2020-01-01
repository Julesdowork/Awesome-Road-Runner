using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class GameData
{
    private int starScore;
    private int scoreCount;
    private bool[] heroes;
    private int selectedIndex;

    public int StarScore
    {
        get { return starScore; }
        set { starScore = value; }
    }

    public int ScoreCount
    {
        get { return scoreCount; }
        set { scoreCount = value; }
    }

    public bool[] Heroes
    {
        get { return heroes; }
        set { heroes = value; }
    }

    public int SelectedIndex
    {
        get { return selectedIndex; }
        set { selectedIndex = value; }
    }
}
