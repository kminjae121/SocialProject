using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private InputSO input;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintMultiplier = 3f;

    private bool _sprint;
    private Vector2 moveInput;

    [SerializeField] private Vector3 moveableVector;
    private Vector3 _center;

    private void Awake()
    {
        input.OnMoveValueChangedEvent += HandleMoveCamera;
        input.OnSprintPressedEvent += HandleSprintControl;

        _center = transform.position; 
    }

    private void OnDestroy()
    {
        input.OnMoveValueChangedEvent -= HandleMoveCamera;
        input.OnSprintPressedEvent -= HandleSprintControl;
    }

    private void Update()
    {
        Vector3 forward = transform.forward;
        forward.y = 0f;
        forward.Normalize();

        Vector3 right = transform.right;
        right.y = 0f;
        right.Normalize();

        float multiSpeedValue = _sprint ? sprintMultiplier : 1f;

        Vector3 moveDir = (right * moveInput.x) + (forward * moveInput.y);
        Vector3 newPos = transform.position + moveDir * moveSpeed * Time.deltaTime * multiSpeedValue;

        Vector3 half = moveableVector * 0.5f;
        newPos.x = Mathf.Clamp(newPos.x, _center.x - half.x, _center.x + half.x);
        newPos.y = Mathf.Clamp(newPos.y, _center.y - half.y, _center.y + half.y);
        newPos.z = Mathf.Clamp(newPos.z, _center.z - half.z, _center.z + half.z);

        transform.position = newPos;
    }

    private void HandleSprintControl(bool value)
    {
        _sprint = value;
    }

    private void HandleMoveCamera(Vector2 vector)
    {
        moveInput = vector;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Application.isPlaying ? _center : transform.position, moveableVector);
    }
}
