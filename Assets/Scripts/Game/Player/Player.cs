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
    private float moveTime = 0.1f;
    private Coroutine corMove = null;
    private Vector2 nextPos = Vector2.zero;
    private List<GameObject> enemys = new List<GameObject>();

    public SpriteRenderer facial;

    private float oddX = 0f;
    private float evenX = -0.75f;
    private float offset = 1.5f;
    // 응집도 낮음 - "몇몇 메서드만이 사용하는 인스턴스 변수", 클린코드 177p
    private float _prevInputDownX;
    public IPlayerInput PlayerInput { get; set; }
    private const float LEFT_SIDE = -1.0f;
    private const float RIGHT_SIDE = 1.0f;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            Init();
        if (!isTouchable) return;
        _prevInputDownX = PlayerInput.GetInputPositionXDown();
        MoveByTouch();
        MoveBySwipe();
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

    public void MoveByTouch()
    {
        corMove = StartCoroutine(TryMovePlayer(CheckTouchLeftOrRight(_prevInputDownX)));
    }

    private float CheckTouchLeftOrRight(float inputPositionX) => inputPositionX - screenCenterX >= 0.0 ? RIGHT_SIDE : LEFT_SIDE;

    public void MoveBySwipe()
    {
        corMove = StartCoroutine(TryMovePlayer(CheckSwipeLeftOrRight(CalcDiffInputDownAndUp())));
    }

    private float CalcDiffInputDownAndUp() => PlayerInput.GetInputPositionXUp() - _prevInputDownX;

    private float CheckSwipeLeftOrRight(float diffInputDownAndUp) => diffInputDownAndUp > 0.0f ? RIGHT_SIDE : LEFT_SIDE;

    // G34: 함수는 추상화 수준을 한 단계만 내려가야 한다.
    private IEnumerator TryMovePlayer(float directionX)
    {
        if (NeedRetryMovePlayer(directionX)) yield break;

        //if (isMoving)
        //    yield break;


        Vector2 startPos = transform.position;
        nextPos = startPos + new Vector2(directionX * offset, 0f);

        if (!CanMoveTo(nextPos))
        {
            corMove = null;
            nextPos = Vector2.zero;
            yield break;
        }
        
        yield return StartCoroutine(ProceedMove(startPos));
    }

    private IEnumerator ProceedMove(Vector2 startPos)
    {
        float timer = 0f;
        while (CheckTimer(timer))
        {
            timer += Time.deltaTime;
            float elapsedTimeRate = timer / moveTime;
            transform.position = Vector2.Lerp(startPos, nextPos, elapsedTimeRate);
            yield return null;
        }
        transform.position = nextPos;
    }

    private bool NeedRetryMovePlayer(float directionX)
    {
        if (!IsBeingMoved()) return false;
        StopAllCoroutines();
        corMove = StartCoroutine(TryMovePlayer(directionX));
        return true;
    }

    private bool IsBeingMoved() => corMove != null;

    private bool CheckTimer(float timer) => timer < moveTime;

    private bool CanMoveTo(Vector2 nextPos)
    {
        enemys = GameManager.Instance.enemyManager.enemys;
        if (IsEnemyEmpty()) return false;
        return !IsOutsideOfValidArea(nextPos);
    }
    private bool IsOutsideOfValidArea(Vector2 nextPos) => IsGoThroughLeftFence(nextPos) || IsGoThroughRightFence(nextPos);

    private bool IsGoThroughRightFence(Vector2 nextPos) => nextPos.x > enemys[enemys.Count - 1].transform.position.x;

    private bool IsGoThroughLeftFence(Vector2 nextPos) => enemys[0].transform.position.x > nextPos.x;

    private bool IsEnemyEmpty() => enemys.Count <= 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
    }
}
