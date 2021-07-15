using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private static float coolTime;
    private static bool isOnCoolTime = false;
    private int rewardLife;
    private static int remainingTime;

    public int currentLife;
    public string lastExitDate;
    public int rewardPeriod;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (isOnCoolTime)
        {
            coolTime += Time.deltaTime;

            if(coolTime >= rewardPeriod)
            {
                currentLife++;
                DataManager.Instance.SetPlayerData("PlayerLife", currentLife);

                if (currentLife >= 20)
                    StopTimer();
            }
        }
    }

    private void Start()
    {
        currentLife = DataManager.Instance.GetPlayerData().playerLife;
        lastExitDate = DataManager.Instance.GetPlayerData().LastExitDate;

        if (currentLife < 20)
        {
            rewardLife = (int)(((DateTime.Now - Convert.ToDateTime(lastExitDate)).TotalSeconds) / 600);
            remainingTime = (int)((DateTime.Now - Convert.ToDateTime(lastExitDate)).TotalSeconds % 600);
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
}

