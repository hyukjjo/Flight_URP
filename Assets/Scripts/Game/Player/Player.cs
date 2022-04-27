using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Figure
{
    [Header("----------Player----------")]
    public bool isTouchable = false;
    //public bool isMoving = false;

    private Vector2 touchPos = Vector2.zero;
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

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            Init();
        float input = PlayerInput.GetInput();
        var movement = transform.position.x + input * Time.deltaTime;
        transform.localPosition = new Vector3(movement, 0f, 0f);
        GetInputAndCheckInputPositionAndMovePlayer();
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
    
    // 인풋 값을 조회해서 그 값으로 왼쪽부 터치인지 오른쪽부 터치인지 판단 후 플레이어 포지션에 반영함
    public void GetInputAndCheckInputPositionAndMovePlayer()
    {
        if (isTouchable)
        {
#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                corMove = StartCoroutine(MovePlayer(CheckInputPosition()));
            }
#elif UNITY_IOS || UNITY_ANDROID
            // 10. 조건부 로직 간소화
            // 함수 - 작게 만들어라! - 블록과 들여쓰기, 클린코드 44p
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                
                if(IsTouchDown(touch))
                {
                    corMove = StartCoroutine(MovePlayer(CheckInputPosition(touch)));
                }
            }
#endif
        }
    }

    private float CheckInputPosition() => Input.mousePosition.x - screenCenterX >= 0.0 ? RIGHT_SIDE : LEFT_SIDE;

    private float CheckInputPosition(Touch touch) => touch.position.x - screenCenterX >= 0.0 ? RIGHT_SIDE : LEFT_SIDE;

    private bool IsTouchDown(Touch touch) => touch.phase == TouchPhase.Began;

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

    private bool CheckNextMove(Vector2 nextPos)
    {
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
