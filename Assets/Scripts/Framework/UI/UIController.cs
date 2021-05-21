using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public Dictionary<string, UIViewBase> viewDic = new Dictionary<string, UIViewBase>();
    public Dictionary<string, UIPopBase> popDic = new Dictionary<string, UIPopBase>();

    private UIViewBase[] views;
    private UIPopBase[] pops;

    // Start is called before the first frame update
    void Start()
    {
        views = GetComponentsInChildren<UIViewBase>(true);
        pops = GetComponentsInChildren<UIPopBase>(true);

        if (views.Length > 0)
        {
            for (int i = 0; i < views.Length; i++)
            {
                viewDic.Add(views[i].name, views[i]);
            }
        }

        if (pops.Length > 0)
        {
            for (int i = 0; i < pops.Length; i++)
            {
                popDic.Add(pops[i].name, pops[i]);
            }
        }
    }
}
