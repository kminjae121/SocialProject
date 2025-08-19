using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GetMousePos : MonoBehaviour
{
    private Vector3 _screenPosition;
    private Vector3 _worldPosition;
    [SerializeField] private LayerMask whatIsGround;


    public event Action OnClicked, OnExit;

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            OnClicked?.Invoke();    
        }
        if(Keyboard.current.escapeKey.wasPressedThisFrame)
            OnExit?.Invoke();
    }

    public bool IsPointerOverUI()
        => EventSystem.current.IsPointerOverGameObject();

    public Vector3 GetWorldPosition()
    {
        Camera mainCam = Camera.main; 
        Debug.Assert(mainCam != null, "No main camera in this scene");
    
        Ray cameraRay = mainCam.ScreenPointToRay(_screenPosition);
        if (Physics.Raycast(cameraRay, out RaycastHit hit, mainCam.farClipPlane, whatIsGround))
        {
            _worldPosition = hit.point;
        }

        return _worldPosition;
    }
}
