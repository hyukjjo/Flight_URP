using System.Collections;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Player_Movement
{
    [UnityTest]
    // 플레이어는_인풋값_1을_받으면_1초에_1미터만큼_이동함
    public IEnumerator player_can_move_1meter_per_1sec_when_take_input_1()
    {
        // Arrange
        var playerInput = Substitute.For<IPlayerInput>();
        var playerGameObject = new GameObject();
        var player = playerGameObject.AddComponent<Player>();
        playerInput.GetInput().Returns(1f);
        player.PlayerInput = playerInput;

        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.SetParent(playerGameObject.transform);
        cube.transform.localPosition = Vector3.zero;

        // Action
        yield return new WaitForSeconds(2f);

        //Assert
        Assert.IsTrue(2.0f <= player.transform.position.x && player.transform.position.x <= 2.1f);
    }
}