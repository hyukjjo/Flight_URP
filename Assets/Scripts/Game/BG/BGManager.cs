using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGManager : MonoBehaviour
{
    [Header("BackGround Setting")]
    public Image BackGround;
    Material mat;
    FigureColor currentColor = FigureColor.RED;

    [Header("BackGroung Objects Setting")]
    public Texture[] shapes;
    public ParticleSystem ps;
    private ParticleSystemRenderer psRenderer;

    private Coroutine corChangeColor = null;
    private float changeTime = 0.5f;
    private FigureShape currentShape = FigureShape.CIRCLE;

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
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SetBackGroundShapeNColor(FigureShape.CIRCLE, FigureColor.RED);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            SetBackGroundShapeNColor(FigureShape.TRIANGLE, FigureColor.GREEN);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            SetBackGroundShapeNColor(FigureShape.SQUARE, FigureColor.BLUE);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            SetBackGroundShapeNColor(FigureShape.PENTAGON, FigureColor.YELLOW);
        if (Input.GetKeyDown(KeyCode.Alpha5))
            SetBackGroundShapeNColor(FigureShape.HEXAGON, FigureColor.GRAY);
#endif
    }

    public void Init()
    {
        currentColor = FigureColor.RED;
        
        mat.SetColor("_TopColor", GetColor(FigureColor.RED) * 0.7f);
        mat.SetColor("_BottomColor", GetColor(FigureColor.RED) * 0.3f);

        psRenderer.material.color = GetColor(FigureColor.RED) * 0.7f;
        psRenderer.trailMaterial.color = GetColor(FigureColor.RED) * 0.3f;

        currentShape = FigureShape.CIRCLE;

        psRenderer.material.mainTexture = shapes[(int)FigureShape.CIRCLE];
    }

    public void SetBackGroundShapeNColor(FigureShape shape, FigureColor color)
    {
        if(corChangeColor == null)
            corChangeColor = StartCoroutine(ChangeColor(shape, color));
    }

    private IEnumerator ChangeColor(FigureShape shape, FigureColor color)
    {
        Color topColor = GetColor(color) * 0.7f;
        Color bottomColor = GetColor(color) * 0.3f;
        psRenderer.material.mainTexture = shapes[(int)shape];

        float totalTime = changeTime;
        float time = 0f;

        while (time < totalTime)
        {
            time += Time.deltaTime;

            mat.SetColor("_TopColor", Color.Lerp(GetColor(currentColor) * 0.7f, topColor, time / totalTime));
            mat.SetColor("_BottomColor", Color.Lerp(GetColor(currentColor) * 0.3f, bottomColor, time / totalTime));
            psRenderer.material.color = Color.Lerp(GetColor(currentColor) * 0.7f, topColor, time / totalTime);
            psRenderer.trailMaterial.color = Color.Lerp(GetColor(currentColor) * 0.3f, bottomColor, time / totalTime);

            yield return null;
        }

        mat.SetColor("_TopColor", topColor);
        mat.SetColor("_BottomColor", bottomColor);
        psRenderer.material.color = topColor;
        psRenderer.trailMaterial.color = bottomColor;

        currentShape = shape;
        currentColor = color;

        corChangeColor = null;
        yield break;
    }

    private Texture GetShape(FigureShape shape)
    {
        Texture tex = null;

        switch (shape)
        {
            case FigureShape.CIRCLE:
                tex = shapes[(int)FigureShape.CIRCLE];
                break;
            case FigureShape.TRIANGLE:
                tex = shapes[(int)FigureShape.TRIANGLE];
                break;
            case FigureShape.SQUARE:
                tex = shapes[(int)FigureShape.SQUARE];
                break;
            case FigureShape.PENTAGON:
            case FigureShape.HEXAGON:
                tex = shapes[(int)FigureShape.PENTAGON];
                break;
            default:
                break;
        }

        return tex;
    }

    private Color GetColor(FigureColor color)
    {
        Color c = Color.clear; ;

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
