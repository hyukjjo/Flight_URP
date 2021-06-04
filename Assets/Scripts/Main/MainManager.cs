using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EMainState
{
    MAIN,
    SHOP,
    RANKING,
    SETTINGS,
    STAGE
}

public class MainManager : MonoBehaviour
{
    public float currentPlayTime = 0f;

    private EMainState mainState = EMainState.MAIN;

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

    public void SetMainState(EMainState state)
    {
        switch (state)
        {
            case EMainState.MAIN:
                break;
            case EMainState.SHOP:
                break;
            case EMainState.RANKING:
                break;
            case EMainState.SETTINGS:
                break;
            case EMainState.STAGE:
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        currentPlayTime += Time.deltaTime;
    }
}
