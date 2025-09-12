using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputSO", menuName = "SO/Input", order = 0)]
public class InputSO : ScriptableObject, Controls.IPlayerActions
{
    private Controls _controlls;

    public event Action<Vector2> OnMoveValueChangedEvent;
    public event Action<Vector2> OnMoveKeyPressedEvent;

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
}
