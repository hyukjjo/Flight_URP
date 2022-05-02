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
    private float diffInputDownAndUp = 0f;
    private float moveTime = 0.1f;
    private Coroutine corMove = null;
    private Vector2 nextPos = Vector2.zero;
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
        Move();
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
    
    public void Move()
    {
        if (!isTouchable) return;
        corMove = StartCoroutine(MovePlayer(CheckLeftOrRight(PlayerInput.GetInputPositionXDown())));
    }
    
    private float CheckLeftOrRight(float inputPositionX) => inputPositionX - screenCenterX >= 0.0 ? RIGHT_SIDE : LEFT_SIDE;
    public void Swipe()
    {
        if (!isTouchable) return;
#if UNITY_EDITOR
        startSwipePos = new Vector2(PlayerInput.GetInputPositionXDown(), 0f);
        // 10.3 조건부 로직 간소화 - 중첩 조건문을 보호 구문으로 바꾸기
        if (Input.GetMouseButtonUp(0))
        {
            endSwipePos = Input.mousePosition;
            diffInputDownAndUp = endSwipePos.x - startSwipePos.x;
            corMove = StartCoroutine(MovePlayer(CheckSwipeLeftOrRight()));

            startSwipePos = Vector2.zero;
            endSwipePos = Vector2.zero;
            diffInputDownAndUp = 0f;
        }
#elif UNITY_IOS || UNITY_ANDROID
        startSwipePos = new Vector2(PlayerInput.GetInputPositionXDown(), 0f);
        // 10.3 조건부 로직 간소화 - 중첩 조건문을 보호 구문으로 바꾸기
        if (IsTouchUp(touch))
        {
            endSwipePos = touch.position;
            diffInputDownAndUp = endSwipePos.x - startSwipePos.x;
            corMove = StartCoroutine(MovePlayer(CheckSwipeLeftOrRight()));

            startSwipePos = Vector2.zero;
            endSwipePos = Vector2.zero;
            diffInputDownAndUp = 0f;
        }
#endif
    }

    private float CheckSwipeLeftOrRight() => diffInputDownAndUp > 0.0f ? RIGHT_SIDE : LEFT_SIDE;

    private bool IsTouchUp(Touch touch)
    {
        return touch.phase == TouchPhase.Ended;
    }

    private IEnumerator MovePlayer(float directionX)
    {
        if (corMove != null)
        {
            transform.position = nextPos;
            nextPos = Vector2.zero;
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
        nextPos = endPos;

        if (!CanMoveTo(nextPos))
        {
            corMove = null;
            nextPos = Vector2.zero;
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
        nextPos = Vector2.zero;
    }

    private bool CanMoveTo(Vector2 nextPos)
    {
        enemys = GameManager.Instance.enemyManager.enemys;
        if (IsEnemyEmpty()) return false;
        return !IsOutsideOfValidArea(nextPos);
    }
    private bool IsOutsideOfValidArea(Vector2 nextPos)
    {
        return enemys[0].transform.position.x > nextPos.x || nextPos.x > enemys[enemys.Count - 1].transform.position.x; ;
    }

    private bool IsEnemyEmpty()
    {
        return enemys.Count <= 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
    }
}
