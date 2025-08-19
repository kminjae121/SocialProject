using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ConstructionSystem : MonoBehaviour
{
    [SerializeField] private GameObject mouseIndicator;
    [SerializeField] private GameObject cellIndicator;
    [SerializeField] private GetMousePos _getMousePos;
    [SerializeField] private Grid _grid;

    [SerializeField] private ObjectDataSO database;
    private int selectObjectIndex = -1;

    [SerializeField] private GameObject gridVisualization;

    private GridData floorData, placeData;

    private Renderer previewRenderer;

    private List<GameObject> placeGameObjects = new();
    
    private void Start()
    {
        StopPlaceMent();
        floorData = new();
        placeData = new();

        previewRenderer = cellIndicator.GetComponentInChildren<Renderer>();
    }

    public void StartPlacement(int ID)
    {
        selectObjectIndex = database.objectData.FindIndex(data =>
            data.ID == ID);

        if (selectObjectIndex < 0)
        {
            return;
        }
        
        gridVisualization.SetActive(true);
        cellIndicator.SetActive(true);
        _getMousePos.OnClicked += PlaceStructure;
        _getMousePos.OnExit += StopPlaceMent;
    }

    private void PlaceStructure()
    {
        if (_getMousePos.IsPointerOverUI())
            return;
        Vector3 mousePosition = _getMousePos.GetWorldPosition();

        Vector3Int gridPosition = _grid.WorldToCell(mousePosition);
        
        bool placementValidity = CheckPlacementValidity(gridPosition, selectObjectIndex);

        if (placementValidity == false)
            return;
        GameObject gameObj = Instantiate(database.objectData[selectObjectIndex].prefab);
        gameObj.transform.position = _grid.CellToWorld(gridPosition);
        placeGameObjects.Add(gameObj);
        GridData selectData = database.objectData[selectObjectIndex].ID  == 0 ? floorData : placeData;
        
        selectData.AddObjectAt(gridPosition,
            database.objectData[selectObjectIndex].size,
            database.objectData[selectObjectIndex].ID,
            placeGameObjects.Count - 1);
    }

    private bool CheckPlacementValidity(Vector3Int gridPosition, int selectObjectIndex)
    {
        GridData selectData = database.objectData[selectObjectIndex].ID  == 0 ? floorData : placeData;

        return selectData.CanPlaceObjectAt(gridPosition, database.objectData[selectObjectIndex].size);
        

    }

    private void StopPlaceMent()
    {
        selectObjectIndex = -1;
        gridVisualization.SetActive(false);
        cellIndicator.SetActive(false);
        _getMousePos.OnClicked -= PlaceStructure;
        _getMousePos.OnExit -= StopPlaceMent;
    }

    private void Update()
    {
        if (selectObjectIndex < 0)
            return;
        
        Vector3 mousePosition = _getMousePos.GetWorldPosition();
        Vector3Int gridPosition = _grid.WorldToCell(mousePosition);
        
        bool placementValidity = CheckPlacementValidity(gridPosition, selectObjectIndex);

        previewRenderer.material.color = placementValidity ? Color.white : Color.red;
        
        
        mouseIndicator.transform.position = mousePosition;
        cellIndicator.transform.position = _grid.CellToWorld(gridPosition);
    }
}
