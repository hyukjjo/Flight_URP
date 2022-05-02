using UnityEngine;

public class MouseInput: IPlayerInput
{
    public float GetInputPositionX()
    {
        if (Input.GetMouseButtonDown(0)) return 0.0f;
        return Input.mousePosition.x;
    }
}