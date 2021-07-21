using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private static float coolTime;
    private static bool isOnCoolTime = false;
    private static int rewardLife;
    private static float remainingTime;

    private static int currentLife;
    private static string lastExitDate;
    private static int _rewardPeriod;
    public int rewardPeriod;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _rewardPeriod = rewardPeriod;
    }

    private void Update()
    {
        if (isOnCoolTime)
        {
            coolTime += Time.deltaTime;

            if (coolTime >= rewardPeriod)
            {
                coolTime = 0f;
                currentLife++;
                DataManager.Instance.SetPlayerData("PlayerLife", currentLife);

                if (currentLife >= 20)
                    StopTimer();
            }
        }
    }

    public static void InitTimer()
    {
        if (PlayerPrefs.GetFloat("CoolTime") > 0f)
        {
            coolTime += PlayerPrefs.GetFloat("CoolTime");
        }

        currentLife = DataManager.Instance.GetPlayerData().playerLife;
        lastExitDate = DataManager.Instance.GetPlayerData().LastExitDate;

        if (currentLife < 20)
        {
            rewardLife = (int)(((DateTime.Now - Convert.ToDateTime(lastExitDate)).TotalSeconds) / _rewardPeriod);
            remainingTime = (int)((DateTime.Now - Convert.ToDateTime(lastExitDate)).TotalSeconds % _rewardPeriod);
            coolTime += remainingTime;
            currentLife += rewardLife;

            if (currentLife > 20)
                currentLife = 20;

            DataManager.Instance.SetPlayerData("PlayerLife", currentLife);
        }
    }

    public static void StartTimer()
    {
        isOnCoolTime = true;
    }

    public static void StopTimer()
    {
        isOnCoolTime = false;
        coolTime = 0f;
    }

    public static float GetTime()
    {
        return coolTime;
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("CoolTime", coolTime);
    }
}