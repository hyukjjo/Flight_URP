using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Figure
{
    [Header("----------Player----------")]
    public bool isTouchable = false;
    //public bool isMoving = false;

    private Vector2 touchPos = Vector2.zero;
    // 응집도 낮음 - "몇몇 메서드만이 사용하는 인스턴스 변수", 클린코드 177p
    private float screenCenterX = Screen.width * 0.5f;
    private Vector2 startSwipePos = Vector2.zero;
    private Vector2 endSwipePos = Vector2.zero;
    private float swipeX = 0f;
    private float moveTime = 0.1f;
    private Coroutine corMove = null;
    private Vector2 moveNextPos = Vector2.zero;
    private List<GameObject> enemys = new List<GameObject>();

    public SpriteRenderer facial;

    private float oddX = 0f;
    private float evenX = -0.75f;
    private float offset = 1.5f;
    public IPlayerInput PlayerInput { get; set; }
    private const float LEFT_SIDE = -1.0f;
    private const float RIGHT_SIDE = 1.0f;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            Init();
        DetectTouchAndMovePlayer();
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
    
    // N4: 명확한 이름
    public void DetectTouchAndMovePlayer()
    {
        if (!isTouchable) return;
        corMove = StartCoroutine(MovePlayer(CheckLeftOrRight(PlayerInput.GetInputPositionX())));
    }
    
    private float CheckLeftOrRight(float inputPositionX) => inputPositionX - screenCenterX >= 0.0 ? RIGHT_SIDE : LEFT_SIDE;
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
                corMove = StartCoroutine(MovePlayer((endSwipePos - startSwipePos).normalized.x));

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
                    corMove = StartCoroutine(MovePlayer((endSwipePos - startSwipePos).normalized.x));

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
        if (corMove != null)
        {
            transform.position = moveNextPos;
            moveNextPos = Vector2.zero;
            StopAllCoroutines();
            corMove = null;
            corMove = StartCoroutine(MovePlayer(directionX));
            yield break;
        }

        //if (isMoving)
        //    yield break;

        float timer = 0f;

        Vector2 startPos = transform.position;
        Vector2 endPos = startPos + new Vector2(directionX * offset, 0f);
        moveNextPos = endPos;

        if (!CheckNextMove(moveNextPos))
        {
            corMove = null;
            moveNextPos = Vector2.zero;
            yield break;
        }

        yield return null;

        while (timer < moveTime)
        {
            timer += Time.deltaTime;
            float flowtime = timer / moveTime;

            transform.position = Vector2.Lerp(startPos, endPos, flowtime);
            yield return null;
        }
        transform.position = endPos;

        corMove = null;
        moveNextPos = Vector2.zero;
    }

    // N4: 명확한 이름
    private bool CheckNextMove(Vector2 nextPos)
    {
        // Stub: 디펜던시 해소
        return true;
        bool move = true;

        if (enemys.Count == 0)
        {
            enemys = GameManager.Instance.enemyManager.enemys;

            if (enemys.Count == 0)
                move = false;
        }

        if (enemys.Count > 0)
        {
            if (nextPos.x < enemys[0].transform.position.x || nextPos.x > enemys[enemys.Count - 1].transform.position.x)
                move = false;
        }

        return move;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
    }
}
