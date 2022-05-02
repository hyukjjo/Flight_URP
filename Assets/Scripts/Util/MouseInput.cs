using UnityEngine;

public class MouseInput: IPlayerInput
{
    public float GetInputPositionXDown() => Input.GetMouseButtonDown(0) ? 0.0f : Input.mousePosition.x;

    public float GetInputPositionXUp() => !Input.GetMouseButtonUp(0) ? 0f : Input.mousePosition.x;
}