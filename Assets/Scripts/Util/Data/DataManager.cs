using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerData
{
    public string startDateTime = string.Empty;
    public string exitDateTime = string.Empty;
    public bool isTutorialDone = false;
    public int playerGold = 0;
    public int playerLife = 20;
    public int stage1_1Stars = 0;
    public int stage1_2Stars = 0;
    public int stage1_3Stars = 0;
    public int stage1_4Stars = 0;
    public int stage1_5Stars = 0;
    public int stage1_6Stars = 0;
    public int stage1_7Stars = 0;
    public int stage1_8Stars = 0;
    public int stage1_9Stars = 0;
    public int stage1_10Stars = 0;
    public int stage2_1Stars = 0;
    public int stage2_2Stars = 0;
    public int stage2_3Stars = 0;
    public int stage2_4Stars = 0;
    public int stage2_5Stars = 0;
    public int stage2_6Stars = 0;
    public int stage2_7Stars = 0;
    public int stage2_8Stars = 0;
    public int stage2_9Stars = 0;
    public int stage2_10Stars = 0;
    public int stage3_1Stars = 0;
    public int stage3_2Stars = 0;
    public int stage3_3Stars = 0;
    public int stage3_4Stars = 0;
    public int stage3_5Stars = 0;
    public int stage3_6Stars = 0;
    public int stage3_7Stars = 0;
    public int stage3_8Stars = 0;
    public int stage3_9Stars = 0;
    public int stage3_10Stars = 0;
    public int stage1_1Score = 0;
    public int stage1_2Score = 0;
    public int stage1_3Score = 0;
    public int stage1_4Score = 0;
    public int stage1_5Score = 0;
    public int stage1_6Score = 0;
    public int stage1_7Score = 0;
    public int stage1_8Score = 0;
    public int stage1_9Score = 0;
    public int stage1_10Score = 0;
    public int stage2_1Score = 0;
    public int stage2_2Score = 0;
    public int stage2_3Score = 0;
    public int stage2_4Score = 0;
    public int stage2_5Score = 0;
    public int stage2_6Score = 0;
    public int stage2_7Score = 0;
    public int stage2_8Score = 0;
    public int stage2_9Score = 0;
    public int stage2_10Score = 0;
    public int stage3_1Score = 0;
    public int stage3_2Score = 0;
    public int stage3_3Score = 0;
    public int stage3_4Score = 0;
    public int stage3_5Score = 0;
    public int stage3_6Score = 0;
    public int stage3_7Score = 0;
    public int stage3_8Score = 0;
    public int stage3_9Score = 0;
    public int stage3_10Score = 0;
}

public class DataManager : MonoBehaviour
{
    private static DataManager instance = null;
    private PlayerData playerData = null;

    public PlayerData GetPlayerData()
    {
        return playerData;
    }

    public void ExportPlayerData(object data)
    {
        string jsonString = JsonUtility.ToJson(data);
        FileStream fileStream = new FileStream("Assets/Resources/Data/PlayerData.json", FileMode.Create);
        StreamWriter streamWriter = new StreamWriter(fileStream);
        streamWriter.Write(jsonString);
        streamWriter.Close();
    }

    public PlayerData ReadPlayerData()
    {
        FileStream fileStream = new FileStream("Assets/Resources/Data/PlayerData.json", FileMode.Open);
        StreamReader streamReader = new StreamReader(fileStream);
        string jsonString = streamReader.ReadToEnd();
        return JsonUtility.FromJson<PlayerData>(jsonString);
    }

    public static DataManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DataManager>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;

        if (playerData == null)
            playerData = new PlayerData();

        DontDestroyOnLoad(gameObject);

        LoadCSV();
        LoadPlayerData();
    }

    private void LoadPlayerData()
    {
        playerData = ReadPlayerData();
    }

    public void SavePlayerData()
    {
        ExportPlayerData(playerData);
    }

    public string file_Stage = "FlightTable_Stage";
    private bool isCSVLoaded = false;

    [HideInInspector]
    public CSVStage stageClass = new CSVStage();

    public void LoadCSV()
    {
        if (isCSVLoaded)
        {
            return;
        }
        else
        {
            if (!string.IsNullOrEmpty(file_Stage))
                stageClass.LoadCSV(file_Stage);

            isCSVLoaded = true;
        }
    }

    private void OnApplicationQuit()
    {
        SavePlayerData();
    }
}
