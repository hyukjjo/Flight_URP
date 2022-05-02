using UnityEngine;

public class MobileInput : IPlayerInput
{
    public float GetInputPositionXDown()
    {
        if (!HasValidTouch()) return 0.0f;
        return Input.GetTouch(0).position.x;
    }
    private bool HasValidTouch() => Input.touchCount > 0 && !IsTouchDown(Input.GetTouch(0));
    private bool IsTouchDown(Touch touch) => touch.phase == TouchPhase.Began;
}