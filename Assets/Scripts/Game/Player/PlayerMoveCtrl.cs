using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveCtrl
{
    private const float LEFT_SIDE = -1.0f;
    private const float RIGHT_SIDE = 1.0f;
    private Player _player;
    private float screenCenterX = Screen.width * 0.5f;
    private float moveTime = 0.1f;
    private Coroutine corMove = null;
    private Vector2 nextPos = Vector2.zero;
    private List<GameObject> enemys = new List<GameObject>();
    private float offset = 1.5f;
    private float _prevInputDownX;

    public PlayerMoveCtrl(Player player)
    {
        _player = player;
    }

    public void ApplyInput(IPlayerInput playerInput)
    {
        _prevInputDownX = playerInput.GetInputPositionXDown();
        MoveByTouch();
        MoveBySwipe();
    }

    private void MoveByTouch()
    {
        corMove = _player.StartCoroutine(TryMovePlayer(CheckTouchLeftOrRight(_prevInputDownX)));
    }

    private float CheckTouchLeftOrRight(float inputPositionX) => inputPositionX - screenCenterX >= 0.0 ? RIGHT_SIDE : LEFT_SIDE;

    private void MoveBySwipe()
    {
        corMove = _player.StartCoroutine(TryMovePlayer(CheckSwipeLeftOrRight(CalcDiffInputDownAndUp())));
    }

    private float CalcDiffInputDownAndUp() => _player.PlayerInput.GetInputPositionXUp() - _prevInputDownX;
    private float CheckSwipeLeftOrRight(float diffInputDownAndUp) => diffInputDownAndUp > 0.0f ? RIGHT_SIDE : LEFT_SIDE;

    private IEnumerator TryMovePlayer(float directionX)
    {
        if (NeedRetryMovePlayer(directionX)) yield break;

        Vector2 startPos = _player.transform.position;
        nextPos = startPos + new Vector2(directionX * offset, 0f);

        if (!CanMoveTo(nextPos))
        {
            corMove = null;
            nextPos = Vector2.zero;
            yield break;
        }
        
        yield return _player.StartCoroutine(ProceedMove(startPos));
    }

    private IEnumerator ProceedMove(Vector2 startPos)
    {
        float timer = 0f;
        while (CheckTimer(timer))
        {
            timer += Time.deltaTime;
            float elapsedTimeRate = timer / moveTime;
            _player.transform.position = Vector2.Lerp(startPos, nextPos, elapsedTimeRate);
            yield return null;
        }

        _player.transform.position = nextPos;
    }

    private bool NeedRetryMovePlayer(float directionX)
    {
        if (!IsBeingMoved()) return false;
        _player.StopAllCoroutines();
        corMove = _player.StartCoroutine(TryMovePlayer(directionX));
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
}