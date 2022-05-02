public class InputFactory
{
    public static IPlayerInput GetPlayerInput()
    {
#if UNITY_EDITOR
        return new MouseInput();
#elif UNITY_IOS || UNITY_ANDROID
        return new MobileInput();
#endif
    }
}


// Stub
public class MouseInput: IPlayerInput
{
    public float GetInputPositionX()
    {
        throw new System.NotImplementedException();
    }
}

// Stub
public class MobileInput : IPlayerInput
{
    public float GetInputPositionX()
    {
        throw new System.NotImplementedException();
    }
}