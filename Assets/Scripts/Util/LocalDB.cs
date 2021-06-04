using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LocalDB
{
    public static bool TutorialDone
    {
        get { return PlayerPrefs.GetInt("Tutorial Done", 0) == 1 ? true : false; }
        set { PlayerPrefs.SetInt("Tutorial Done", value ? 1 : 0); }
    }

    public static int PlayerGold
    {
        get { return PlayerPrefs.GetInt("Player Gold", 0); }
        set { PlayerPrefs.SetInt("Player Gold", value); }
    }

    public static int PlayerScore
    {
        get { return PlayerPrefs.GetInt("Player Score", 0); }
        set { PlayerPrefs.SetInt("Player Score", value); }
    }

    public static int PlayerLife
    {
        get { return PlayerPrefs.GetInt("Player Life", 0); }
        set { PlayerPrefs.SetInt("Player Life", value); }
    }
}
