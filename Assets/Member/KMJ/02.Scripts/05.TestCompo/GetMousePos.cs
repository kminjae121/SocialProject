using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GetMousePos : MonoBehaviour
{
    [SerializeField] private Camera _sceneCam;
    private Vector3 lastPosition;
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
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = _sceneCam.nearClipPlane;
        Ray ray = _sceneCam.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, whatIsGround))
        {
            lastPosition = hit.point;
        }

        return lastPosition;
    }
}
