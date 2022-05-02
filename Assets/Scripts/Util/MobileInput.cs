using UnityEngine;

public class MobileInput : IPlayerInput
{
    public float GetInputPositionXDown() => !HasValidTouchDown() ? 0.0f : Input.GetTouch(0).position.x;

    private bool HasValidTouchDown() => Input.touchCount > 0 && IsTouchDown(Input.GetTouch(0));

    private bool IsTouchDown(Touch touch) => touch.phase == TouchPhase.Began;

    public float GetInputPositionXUp() => !HasValidTouchUp() ? 0f : Input.GetTouch(0).position.x;

    private bool HasValidTouchUp() => Input.touchCount > 0 && IsTouchUp(Input.GetTouch(0));

    private bool IsTouchUp(Touch touch) => touch.phase == TouchPhase.Ended;
}