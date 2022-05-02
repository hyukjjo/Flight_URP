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
    // 불필요한 코드(Redundant Code)
    private Vector2 startSwipePos = Vector2.zero;
    // 불필요한 코드(Redundant Code)
    private Vector2 endSwipePos = Vector2.zero;
    // 불필요한 코드(Redundant Code)
    private float diffInputDownAndUp = 0f;
    private float moveTime = 0.1f;
    private Coroutine corMove = null;
    private Vector2 nextPos = Vector2.zero;
    private List<GameObject> enemys = new List<GameObject>();

    public SpriteRenderer facial;

    private float oddX = 0f;
    private float evenX = -0.75f;
    private float offset = 1.5f;
    private float _prevInputDownX;
    public IPlayerInput PlayerInput { get; set; }
    private const float LEFT_SIDE = -1.0f;
    private const float RIGHT_SIDE = 1.0f;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            Init();
        _prevInputDownX = PlayerInput.GetInputPositionXDown();
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
    // N4: 명확한 이름
    public void Move()
    {
        if (!isTouchable) return;
        corMove = StartCoroutine(MovePlayer(CheckLeftOrRight(_prevInputDownX)));
    }
    // N4: 명확한 이름
    private float CheckLeftOrRight(float inputPositionX) => inputPositionX - screenCenterX >= 0.0 ? RIGHT_SIDE : LEFT_SIDE;
    // N4: 명확한 이름
    public void Swipe()
    {
        if (!isTouchable) return;
        diffInputDownAndUp = PlayerInput.GetInputPositionXUp() - _prevInputDownX;
        corMove = StartCoroutine(MovePlayer(CheckSwipeLeftOrRight()));
        // 불필요한 코드(Redundant Code)
        ResetDiffInputDownAndUp();
    }
    // 불필요한 코드(Redundant Code)
    private void ResetDiffInputDownAndUp()
    {
        startSwipePos = Vector2.zero;
        endSwipePos = Vector2.zero;
        diffInputDownAndUp = 0f;
    }
    // 불필요한 코드(Redundant Code)
    private float GetDiffInputDownAndUp()
    {
#if UNITY_EDITOR
        startSwipePos = new Vector2(PlayerInput.GetInputPositionXDown(), 0f);
        endSwipePos = new Vector2(PlayerInput.GetInputPositionXUp(), 0f);
        return endSwipePos.x - startSwipePos.x;
#elif UNITY_IOS || UNITY_ANDROID
        startSwipePos = new Vector2(PlayerInput.GetInputPositionXDown(), 0f);
        endSwipePos = new Vector2(PlayerInput.GetInputPositionXUp(), 0f);
        return endSwipePos.x - startSwipePos.x;
#endif
    }

    private float CheckSwipeLeftOrRight() => diffInputDownAndUp > 0.0f ? RIGHT_SIDE : LEFT_SIDE;

    // G34: 함수는 추상화 수준을 한 단계만 내려가야 한다.
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
