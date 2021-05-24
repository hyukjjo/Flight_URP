using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIPopBase : MonoBehaviour
{
    private void OnEnable()
    {
        InitPop();
    }

    private void OnDisable()
    {
        ResetPop();
    }

    public abstract void InitPop();
    public abstract void ResetPop();
}
