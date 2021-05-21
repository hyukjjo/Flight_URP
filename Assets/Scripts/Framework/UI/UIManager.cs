using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance = null;
    public static UIManager GetInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
            }
            return instance;
        }
    }

    public UIController rootUI;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ActiveView(string name)
    {
        if(rootUI != null)
        {
            rootUI.viewDic[name].gameObject.SetActive(true);
        }
    }

    public void ActivePop(string name)
    {
        if (rootUI != null)
        {
            rootUI.popDic[name].gameObject.SetActive(true);
        }
    }

    public void HideView(string name)
    {
        if (rootUI != null)
        {
            rootUI.viewDic[name].gameObject.SetActive(false);
        }
    }

    public void HidePop(string name)
    {
        if (rootUI != null)
        {
            rootUI.viewDic[name].gameObject.SetActive(false);
        }
    }

    public void HideAllView()
    {
        if (rootUI != null)
        {
            foreach(UIViewBase view in rootUI.viewDic.Values)
            {
                view.gameObject.SetActive(false);
            }
        }
    }

    public void HideAllPop()
    {
        if (rootUI != null)
        {
            foreach (UIPopBase pop in rootUI.popDic.Values)
            {
                pop.gameObject.SetActive(false);
            }
        }
    }
}
