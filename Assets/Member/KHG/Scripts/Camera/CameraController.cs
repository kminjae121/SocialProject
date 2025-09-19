using System;
using UnityEngine;
using Unity.Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private InputSO input;
    [SerializeField] private Transform movePointTrm;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintMultiplier = 3f;

    [SerializeField] private float zoomSpeed = 2f;
    [SerializeField] private float minOrthoSize = 2f;
    [SerializeField] private float maxOrthoSize = 50f;
    [SerializeField] private float minFov = 20f;
    [SerializeField] private float maxFov = 100f;

    private bool _sprint;
    private Vector2 moveInput;
    private Vector3 _center;

    [SerializeField] private CinemachineCamera _vcam;
    [SerializeField] private Vector3 moveableVector;

    private float _referenceOrthoSize;
    private float _referenceFov;

    private void Awake()
    {
        input.OnMoveValueChangedEvent += HandleMoveCamera;
        input.OnSprintPressedEvent += HandleSprintControl;
        input.OnScrollEvent += HandleScroll;

        _center = movePointTrm.position;

        if (_vcam != null)
        {
            var lens = _vcam.Lens;
            _referenceOrthoSize = lens.OrthographicSize;
            _referenceFov = lens.FieldOfView;
        }
    }

    private void OnDestroy()
    {
        input.OnMoveValueChangedEvent -= HandleMoveCamera;
        input.OnSprintPressedEvent -= HandleSprintControl;
        input.OnScrollEvent -= HandleScroll;
    }

    private void Update()
    {
        if (_vcam == null) return;

        var lens = _vcam.Lens;

        // 줌 상태에 따른 스케일 보정
        float zoomScale = 1f;
        if (lens.Orthographic)
            zoomScale = lens.OrthographicSize / _referenceOrthoSize;
        else
            zoomScale = lens.FieldOfView / _referenceFov;

        float currentSpeed = moveSpeed * zoomScale;
        if (_sprint)
            currentSpeed *= sprintMultiplier;

        Vector3 forward = transform.forward;
        forward.y = 0f;
        forward.Normalize();

        Vector3 right = transform.right;
        right.y = 0f;
        right.Normalize();

        Vector3 moveDir = (right * moveInput.x) + (forward * moveInput.y);
        Vector3 newPos = transform.position + moveDir * currentSpeed * Time.deltaTime;

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

    private void HandleScroll(float scrollValue)
    {
        if (_vcam == null) return;

        var lens = _vcam.Lens;

        if (lens.Orthographic)
        {
            lens.OrthographicSize -= scrollValue * zoomSpeed * Time.deltaTime;
            lens.OrthographicSize = Mathf.Clamp(lens.OrthographicSize, minOrthoSize, maxOrthoSize);
        }
        else
        {
            lens.FieldOfView -= scrollValue * zoomSpeed * Time.deltaTime;
            lens.FieldOfView = Mathf.Clamp(lens.FieldOfView, minFov, maxFov);
        }

        _vcam.Lens = lens;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Application.isPlaying ? _center : movePointTrm.position, moveableVector);
    }
}
