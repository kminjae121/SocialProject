using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Serialization;

public class SetCantPlacement : MonoBehaviour
{
    [SerializeField] private GridData _gridData;

    [SerializeField] private List<Vector3Int> _waterArea;
    
    [SerializeField] private List<Vector3Int> _mountainArea;
    
    [SerializeField] private List<Vector3Int> _roadArea;
    private void Start()
    {
        SetCantPlaceObjectArea();
    }

    private void SetCantPlaceObjectArea()
    {
        _gridData.SetCantPlaceObjectAt(_waterArea, CantPlacePoint.Water);
        _gridData.SetCantPlaceObjectAt(_mountainArea, CantPlacePoint.Mountain);
        _gridData.SetCantPlaceObjectAt(_roadArea, CantPlacePoint.Road);
    }
}
