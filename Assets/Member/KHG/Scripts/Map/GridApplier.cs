using System;
using Core.Events;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class GridApplier : MonoBehaviour
{
    [SerializeField] private GameEventChannelSO mapChannel;
    [SerializeField] private Material _gridMaterial;
    [SerializeField] private Material _normalMaterial;

    private MeshRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
        _renderer.material = _normalMaterial;
        mapChannel.AddListener<GridMaterialEvent>(HandleGridVisual);
    }

    private void OnDestroy()
    {
        mapChannel.RemoveListener<GridMaterialEvent>(HandleGridVisual);
    }

    private void HandleGridVisual(GridMaterialEvent evt)
    {
        print("grid 이벤트 받음");
        if(evt.Enabled)
            _renderer.material = _gridMaterial;
        else
            _renderer.material = _normalMaterial;
    }
}
