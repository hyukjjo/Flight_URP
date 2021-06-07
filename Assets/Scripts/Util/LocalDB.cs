using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LocalDB
{
    public static bool TutorialDone
    {
        get { return PlayerPrefs.GetInt("TutorialDone", 0) == 1; }
        set { PlayerPrefs.SetInt("TutorialDone", value ? 1 : 0); }
    }

    public static int PlayerGold
    {
        get { return PlayerPrefs.GetInt("PlayerGold", 0); }
        set { PlayerPrefs.SetInt("PlayerGold", value); }
    }

    public static int PlayerScore
    {
        get { return PlayerPrefs.GetInt("PlayerScore", 0); }
        set { PlayerPrefs.SetInt("PlayerScore", value); }
    }

    public static int PlayerLife
    {
        get { return PlayerPrefs.GetInt("PlayerLife", 20); }
        set { PlayerPrefs.SetInt("PlayerLife", value); }
    }
}