using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    private Vector2 initPos;
    private bool isVisible = false;

    public Action CallBackEnd;

    private void Awake()
    {
        initPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 0.5f, Screen.height * 2f, 0f));
        enabled = false;
    }

    void Start()
    {
        
    }

    void Update()
    {
        transform.position += Vector3.down * GameManager.Instance.EnemySpeed;
    }

    private void OnEnable()
    {
        transform.position = initPos;
    }

    private void OnDisable()
    {
        transform.position = initPos;
    }

    private void OnBecameVisible()
    {
        isVisible = true;
    }

    private void OnBecameInvisible()
    {
        if (isVisible)
        {
            transform.position = initPos;
            CallBackEnd();
        }
    }

    public void Init()
    {

    }
}
