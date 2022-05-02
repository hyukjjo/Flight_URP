using UnityEngine;

public class MouseInput: IPlayerInput
{
    public float GetInputPositionXDown()
    {
        if (Input.GetMouseButtonDown(0)) return 0.0f;
        return Input.mousePosition.x;
    }
}