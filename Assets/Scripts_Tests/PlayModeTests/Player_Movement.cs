using System;
using System.Collections;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Player_Movement
{
    [UnityTest]
    // 플레이어는 왼쪽의 X축 인풋을 조회하고 왼쪽으로 움직일 수 있다.
    public IEnumerator player_can_get_inputPositionX_left_and_move_left()
    {
        // Arrange
        var playerInput = Substitute.For<IPlayerInput>();
        var playerGameObject = new GameObject();
        var player = playerGameObject.AddComponent<Player>();
        playerInput.GetInputPositionX().Returns(-1f);
        player.PlayerInput = playerInput;
        player.isTouchable = true;  // Stub: 디펜던시 해소

        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.SetParent(playerGameObject.transform);
        cube.transform.localPosition = Vector3.zero;

        float prevPositionX = player.transform.position.x;

        // Action
        yield return new WaitForSeconds(2f);

        //Assert
        Assert.IsTrue(Math.Abs(player.transform.position.x - prevPositionX) > 1.0f);
    }
}