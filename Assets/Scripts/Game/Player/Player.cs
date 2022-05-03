using UnityEngine;

public class Player : Figure
{
    [Header("----------Player----------")]
    public bool isTouchable = false;
    public SpriteRenderer facial;

    private float oddX = 0f;
    private float evenX = -0.75f;
    private PlayerMoveCtrl _playerMoveCtrl;
    private IPlayerInput _playerInput;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            Init();
        if (!isTouchable) return;
        _playerMoveCtrl.ApplyInput();
    }

    public override void Init()
    {
        base.Init();
        _playerInput = InputFactory.GetPlayerInput();
        _playerMoveCtrl = new PlayerMoveCtrl(this, _playerInput);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
    }
}
