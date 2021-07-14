using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum EMainState
{
    MAIN,
    SHOP,
    RANKING,
    SETTINGS,
    STAGE,
    MISSION
}

public class MainManager : MonoBehaviour
{
    public static Action mainCallback;
    public static Action shopCallback;
    public static Action rankingCallback;
    public static Action settingsCallback;
    public static Action stageCallback;
    public static Action missionCallback;

    private static EMainState mainState = EMainState.MAIN;

    private void Awake()
    {
        if(Application.platform.Equals(RuntimePlatform.IPhonePlayer))
        {
            Application.targetFrameRate = 60;
        }
    }

    public static void SetMainState(EMainState state)
    {
        switch (state)
        {
            case EMainState.MAIN:
                mainState = EMainState.MAIN;
                mainCallback?.Invoke();
                break;
            case EMainState.SHOP:
                mainState = EMainState.SHOP;
                shopCallback?.Invoke();
                break;
            case EMainState.RANKING:
                mainState = EMainState.RANKING;
                rankingCallback?.Invoke();
                break;
            case EMainState.SETTINGS:
                mainState = EMainState.SETTINGS;
                settingsCallback?.Invoke();
                break;
            case EMainState.STAGE:
                mainState = EMainState.STAGE;
                stageCallback?.Invoke();
                break;
            case EMainState.MISSION:
                mainState = EMainState.MISSION;
                missionCallback?.Invoke();
                break;
            default:
                break;
        }
    }
}