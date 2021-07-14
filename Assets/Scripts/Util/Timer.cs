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
            coolTime += Time.deltaTime;
    }

    private void Start()
    {
        currentLife = DataManager.Instance.GetPlayerData().playerLife;
        lastExitDate = DataManager.Instance.GetPlayerData().LastExitDate;

        if (currentLife < 20)
        {
            rewardLife = (int)((Convert.ToDateTime(lastExitDate) - DateTime.Now).TotalSeconds / 60.0f);
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

