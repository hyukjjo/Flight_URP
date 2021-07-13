using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }

    //3 = 5, 4 = 6, 5 = 7 CameraSize
    public const int StageCameraSize = 5;




    public GameState currentState;

    public Player player;
    public EnemyManager enemyManager;
    public GameUI gameUI;
    public int stage = 1;
    public int Stage { get { return stage; } set { stage = value; } }

    public int enemyCount = 3;
    public int EnemyCount { get { return enemyCount; } set { enemyCount = value; } }

    public float enemySpeed = 0.1f;
    public float EnemySpeed { get { return enemySpeed; } set { enemySpeed = value; } }






    public Sprite[] models;
    public Sprite[] facials;


    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = -1;

        Init();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init()
    {
        enemyCount = StageInfo.Instance.figureCount;
        enemySpeed = StageInfo.Instance.speedIncrement;

        player.Init();
        gameUI.Init();
    }

    private void SetState(GameState state)
    {
        currentState = state;
    }

    public void SetReady()
    {
        SetState(new GameState_Ready());
    }

    public void SetTutorial()
    {
        SetState(new GameState_Tutorial());
    }

    public void SetPlay()
    {
        SetState(new GameState_Play());
    }

    public void SetGameOver()
    {
        SetState(new GameState_GameOver());
    }
}
