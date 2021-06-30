using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Figure
{
    [Header("----------Player----------")]
    public bool isTouchable = false;
    public bool isMoving = false;

    private Vector2 touchPos = Vector2.zero;
    private float screenCenterX = Screen.width * 0.5f;
    private Vector2 startSwipePos = Vector2.zero;
    private Vector2 endSwipePos = Vector2.zero;
    private float swipeX = 0f;
    private float moveTime = 0.1f;

    public SpriteRenderer facial;

    private float oddX = 0f;
    private float evenX = -0.75f;
    private float offset = 1.5f;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            Init();

        Touch();
        //Swipe();
    }

    public override void Init()
    {
        base.Init();

        SetPosition();
        figure = FigureData.GetRandomData(GameManager.Instance.EnemyCount);
        SetFigure(figure);
    }

    public override void SetPosition()
    {
        base.SetPosition();

        transform.localPosition = new Vector2(GameManager.Instance.EnemyCount % 2 == 0 ? evenX : oddX, -4f);
    }

    public override void SetFigure(FigureData data)
    {
        base.SetFigure(data);

        model.sprite = GameManager.Instance.models[(int)figure.shape];
    }

    public void Touch()
    {
        if (isTouchable)
        {
#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                touchPos = Input.mousePosition;
                StartCoroutine(MovePlayer(Mathf.Sign(touchPos.x - screenCenterX)));

                touchPos = Vector2.zero;
            }
#elif UNITY_IOS || UNITY_ANDROID
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if(touch.phase == TouchPhase.Began)
                {
                    startSwipePos = touch.position;
                    StartCoroutine(MovePlayer(Mathf.Sign(touchPos.x - screenCenterX)));

                    touchPos = Vector2.zero;
                }
            }
#endif
        }
    }

    public void Swipe()
    {
        if (isTouchable)
        {
#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
                startSwipePos = Input.mousePosition;
            else if (Input.GetMouseButtonUp(0))
            {
                endSwipePos = Input.mousePosition;
                swipeX = endSwipePos.x - startSwipePos.x;
                StartCoroutine(MovePlayer((endSwipePos - startSwipePos).normalized.x));

                startSwipePos = Vector2.zero;
                endSwipePos = Vector2.zero;
                swipeX = 0f;
            }
#elif UNITY_IOS || UNITY_ANDROID
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    startSwipePos = touch.position;
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    endSwipePos = touch.position;
                    swipeX = endSwipePos.x - startSwipePos.x;
                    StartCoroutine(MovePlayer((endSwipePos - startSwipePos).normalized.x));

                    startSwipePos = Vector2.zero;
                    endSwipePos = Vector2.zero;
                    swipeX = 0f;
                }
            }
#endif
        }
    }

    private IEnumerator MovePlayer(float directionX)
    {
        if (isMoving)
            yield break;

        float timer = 0f;

        Vector2 startPos = transform.position;
        Vector2 endPos = startPos + new Vector2(directionX * offset, 0f);

        yield return null;

        while (timer < moveTime)
        {
            timer += Time.deltaTime;
            float flowtime = timer / moveTime;

            transform.position = Vector2.Lerp(startPos, endPos, flowtime);
            yield return null;
        }
        transform.position = endPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
    }
}
