using NUnit.Framework;

[TestFixture]
public class Player_Movement
{
    [Test, Description("플레이어는 InputFactory를 통해 PlayerInput을 주입 받을 수 있다.")]
    public void player_can_be_injected_with_PlayerInput_via_InputFactory()
    {
        IPlayerInput playerInput = InputFactory.GetPlayerInput();

        Assert.IsTrue(playerInput != null);
    }
}