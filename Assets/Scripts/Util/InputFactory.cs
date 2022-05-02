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
