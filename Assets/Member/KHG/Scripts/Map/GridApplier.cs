using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class GridApplier : MonoBehaviour
{
    [SerializeField] private Material _gridMaterial;
    [SerializeField] private Material _normalMaterial;

    private MeshRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
    }

    private void HandleGridVisual()
    {
        //if()
    }
}
