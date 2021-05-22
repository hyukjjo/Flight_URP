using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameState currentState;

    private void Awake()
    {
        
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
