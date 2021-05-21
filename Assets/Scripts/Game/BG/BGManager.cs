using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGManager : MonoBehaviour
{
    [Header("BackGround Setting")]
    public Image BackGround;
    Material mat;
    FigureColor currentColor = FigureColor.GRAY;

    [Header("BackGroung Objects Setting")]
    public Transform bgObjectParent;
    List<BGFigure> bGFigures = new List<BGFigure>();

    public ParticleSystem ps;
    private ParticleSystemRenderer psRenderer;

    private void Awake()
    {
        if (BackGround)
            mat = BackGround.material;
        if (ps)
            psRenderer = ps.GetComponent<ParticleSystemRenderer>();




        Init();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SetBackGroundColor(FigureColor.RED);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            SetBackGroundColor(FigureColor.GREEN);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            SetBackGroundColor(FigureColor.BLUE);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            SetBackGroundColor(FigureColor.YELLOW);
        if (Input.GetKeyDown(KeyCode.Alpha5))
            SetBackGroundColor(FigureColor.GRAY);
    }

    public void Init()
    {
        mat.SetColor("_TopColor", GetColor(FigureColor.RED) * 0.7f);
        mat.SetColor("_BottomColor", GetColor(FigureColor.RED) * 0.3f);

        currentColor = FigureColor.RED;

        psRenderer.material.color = GetColor(FigureColor.RED) * 0.7f;
        psRenderer.trailMaterial.color = GetColor(FigureColor.RED) * 0.3f;
    }

    public void SetBackGroundColor(FigureColor color)
    {
        Color topColor = GetColor(color) * 0.7f;
        Color bottomColor = GetColor(color) * 0.3f;

        mat.SetColor("_TopColor", topColor);
        mat.SetColor("_BottomColor", bottomColor);
        psRenderer.material.color = topColor;
        psRenderer.trailMaterial.color = bottomColor; ;

        currentColor = color;
    }

    private Color GetColor(FigureColor color)
    {
        Color c = Color.clear;

        switch (color)
        {
            case FigureColor.RED:
                c = Color.red;
                break;
            case FigureColor.GREEN:
                c = Color.green;
                break;
            case FigureColor.BLUE:
                c = Color.blue;
                break;
            case FigureColor.YELLOW:
                c = Color.yellow;
                break;
            case FigureColor.GRAY:
                c = Color.gray;
                break;
            default:
                break;
        }

        return c;
    }
}
