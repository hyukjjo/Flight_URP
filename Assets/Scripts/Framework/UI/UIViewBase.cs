using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIViewBase : MonoBehaviour
{
    private void OnEnable()
    {
        InitView();
    }

    private void OnDisable()
    {
        ResetView();
    }

    public abstract void InitView();
    public abstract void ResetView();
}
