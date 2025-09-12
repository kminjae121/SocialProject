using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private InputSO input;
    [SerializeField] private float moveSpeed = 5f;

    private Vector2 moveInput;

    private void Awake()
    {
        input.OnMoveValueChangedEvent += HandleMoveCamera;
    }

    private void OnDestroy()
    {
        input.OnMoveValueChangedEvent -= HandleMoveCamera;
    }

    private void Update()
    {
        Vector3 forward = transform.forward;
        forward.y = 0f;
        forward.Normalize();

        Vector3 right = transform.right;
        right.y = 0f;
        right.Normalize();

        Vector3 moveDir = (right * moveInput.x) + (forward * moveInput.y);

        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    private void HandleMoveCamera(Vector2 vector)
    {
        moveInput = vector;
    }
}
