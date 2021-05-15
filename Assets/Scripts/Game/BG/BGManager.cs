using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGManager : MonoBehaviour
{
    [Header("BackGround Setting")]
    public Image BackGround;
    Material mat;

    [Header("BackGroung Objects Setting")]
    public Transform bgObjectParent;
    List<BGFigure> bGFigures = new List<BGFigure>();
    List<BGFeverStar> bGFeverStars = new List<BGFeverStar>();

    private void Awake()
    {
        if (BackGround)
            mat = BackGround.material;




        Init();
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
}
