using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputSO", menuName = "SO/Input", order = 0)]
public class InputSO : ScriptableObject, Controls.IPlayerActions
{
    private Controls _controlls;

    public event Action<Vector2> OnMoveValueChangedEvent;
    public event Action<Vector2> OnMoveKeyPressedEvent;

    public event Action<float> OnScrollEvent;
    public event Action<bool> OnSprintPressedEvent;

    public Vector2 MovementDirection { get; private set; }

    private void OnEnable()
    {
        if (_controlls == null)
        {
            _controlls = new Controls();
            _controlls.Player.SetCallbacks(this);
        }
        _controlls.Player.Enable();
    }

    private void OnDisable()
    {
        _controlls?.Player.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 movementInputVector = context.ReadValue<Vector2>();

        if (context.performed)
            OnMoveKeyPressedEvent?.Invoke(movementInputVector);

        MovementDirection = movementInputVector;
        OnMoveValueChangedEvent?.Invoke(MovementDirection);
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnSprintPressedEvent?.Invoke(true);
        if (context.canceled)
            OnSprintPressedEvent?.Invoke(false);
    }

    public void OnScroll(InputAction.CallbackContext context)
    {
        float scrollValue = context.ReadValue<Vector2>().y;
        if (Mathf.Abs(scrollValue) > 0.01f)
            OnScrollEvent?.Invoke(scrollValue);
    }

}
