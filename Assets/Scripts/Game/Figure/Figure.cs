using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Figure : MonoBehaviour
{
    public FigureData figure;

    public SpriteRenderer model;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Init()
    {
        
    }

    public virtual void SetPosition()
    {

    }

    public virtual void SetFigure(FigureData data)
    {
        figure = data;
    }
}
