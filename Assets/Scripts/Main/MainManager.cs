using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    protected GameState gameState;

    private void Awake()
    {
        if(Application.platform.Equals(RuntimePlatform.IPhonePlayer))
        {
            Application.targetFrameRate = 60;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetState(new GameState_Tutorial());
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetState(new GameState_Ready());
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetState(new GameState_Play());
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetState(new GameState_GameOver());
        }

    }

    protected void SetState(GameState state)
    {
        gameState = state;
    }
}
