using System;
using System.Collections.Generic;
using Core.Events;
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

    [SerializeField] private GridData placeData;

    private Renderer previewRenderer;

    private List<GameObject> placeGameObjects = new();
    [SerializeField] private LayerMask _whatIsStructor;
    [SerializeField] private LayerMask _whatIsConstruction;
    [SerializeField] private GameEventChannelSO _mapChannel;

    private bool _isTopSpawning = false;

    private bool _isSpawning = false;
    
    private void Start()
    {
        StopPlaceMent();
        
        previewRenderer = cellIndicator.GetComponentInChildren<Renderer>();
    }

    public bool DetectedObject(ObjectData objData)
    {
        Collider[] collider = Physics.OverlapBox(transform.position, objData.DetectedRangeVec, Quaternion.identity,
            objData.DetectedLayer);

        if (collider != null)
            return true;

        return false;
    }

    public void StartPlacement(int ID)
    {
        _mapChannel.RaiseEvent(MapEvents.GridMaterialEvent.Initialize(true));
        _isSpawning = true;
        StopPlaceMent();
        
        selectObjectIndex = database.objectData.FindIndex(data =>
            data.ID == ID);

        if (database.objectData[selectObjectIndex].Name == "SunFactory")
        {
            ConstructTopFactory();
            return;
        }
        
        
        if (database.objectData[selectObjectIndex].price > ResourceManager.Instance.Satisfaction)
        {
            return;
        }        
        
        if (selectObjectIndex < 0)
        {
            return;
        }
        
        gridVisualization.SetActive(true);
        cellIndicator.SetActive(true);
        
        GameObject cellChanger = Instantiate(database.objectData[selectObjectIndex].visualPrefab);
        
        cellIndicator = cellChanger;
        
        previewRenderer = cellIndicator.GetComponentInChildren<Renderer>();
        
        _getMousePos.OnClicked += PlaceStructure;
        _getMousePos.OnExit += StopPlaceMent;
    }

    private void ConstructTopFactory()
    {
        _isTopSpawning = true;
        gridVisualization.SetActive(true);
        cellIndicator.SetActive(true);
        
        GameObject cellChanger = Instantiate(database.objectData[selectObjectIndex].visualPrefab);
        
        cellIndicator = cellChanger;
        
        previewRenderer = cellIndicator.GetComponentInChildren<Renderer>();
        
        _getMousePos.OnClicked += PlaceTop;
        _getMousePos.OnExit += StopPlaceMent;
    }
    
    private void PlaceTop()
    {
        if (_getMousePos.IsPointerOverUI())
            return;

        if (ResourceManager.Instance.CanConstructionObject(database.objectData[selectObjectIndex].price))
        {
            ResourceManager.Instance.ReduceSatisfaction(database.objectData[selectObjectIndex].price);
        }
        else
            return;
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (((1 << hit.collider.gameObject.layer) & _whatIsConstruction) != 0)
            {
                GameObject gameObj = Instantiate(database.objectData[selectObjectIndex].prefab, hit.point, Quaternion.identity);
                placeGameObjects.Add(gameObj);

                gameObj.GetComponent<ConstructionObject>().StartConstructionObject();
            }
        }
    }

    private bool IsTopSpawnning()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        return Physics.Raycast(ray,int.MaxValue,_whatIsConstruction);
    }

    public void DestroyPlacement()
    {
        _getMousePos.OnClicked -= PlaceStructure;
        _getMousePos.OnExit -= StopPlaceMent;

        _getMousePos.OnClicked += DestoryStructure;
        _getMousePos.OnExit += StopDestory;
    }

    private void DestoryStructure()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit))
        {
            if (((1 << hit.collider.gameObject.layer) & _whatIsStructor) != 0)
            {
                Vector3Int gridPosition = _grid.WorldToCell(hit.collider.gameObject.transform.position);
                placeData.DestroyObject(gridPosition);
                hit.collider.gameObject.SetActive(false);
            }
        }
        
        
    }

    private void StopDestory()
    {
        _isTopSpawning = false;
        _getMousePos.OnClicked -= DestoryStructure;
        _getMousePos.OnExit -= StopPlaceMent;
    }

    private void PlaceStructure()
    {
        if (_getMousePos.IsPointerOverUI())
            return;

        if (ResourceManager.Instance.CanConstructionObject(database.objectData[selectObjectIndex].price))
        {
            ResourceManager.Instance.ReduceSatisfaction(database.objectData[selectObjectIndex].price);
        }
        else
            return;
        
        
        Vector3 mousePosition = _getMousePos.GetWorldPosition();

        Vector3Int gridPosition = _grid.WorldToCell(mousePosition);
        
        bool placementValidity = CheckPlacementValidity(gridPosition, selectObjectIndex);

        if (placementValidity == false)
            return;
        GameObject gameObj = Instantiate(database.objectData[selectObjectIndex].prefab);
        gameObj.transform.position = _grid.CellToWorld(gridPosition);
        placeGameObjects.Add(gameObj);

        gameObj.GetComponent<ConstructionObject>().StartConstructionObject();
        DetectedObject(database.objectData[selectObjectIndex]);
        
        
        placeData.AddObjectAt(gridPosition,
            database.objectData[selectObjectIndex].size,
            database.objectData[selectObjectIndex].ID,
            placeGameObjects.Count - 1);
    }

    private bool CheckPlacementValidity(Vector3Int gridPosition, int selectObjectIndex)
    {
        GridData selectData = placeData;

        return selectData.CanPlaceObjectAt(gridPosition, database.objectData[selectObjectIndex].size);
    }

    private void StopPlaceMent()
    {
        _mapChannel.RaiseEvent(MapEvents.GridMaterialEvent.Initialize(false));
        _isSpawning = false;
        _isTopSpawning = false;
        selectObjectIndex = -1;
        gridVisualization.SetActive(false);
        cellIndicator.SetActive(false);
        _getMousePos.OnClicked -= PlaceStructure;
        _getMousePos.OnExit -= StopPlaceMent;
    }

    private void Update()
    {
        Vector3 mousePosition = _getMousePos.GetWorldPosition();
        Vector3Int gridPosition = _grid.WorldToCell(mousePosition);

        mouseIndicator.transform.position = mousePosition;
        
        Vector3 cellWorldPos = _grid.CellToWorld(gridPosition);
        
        Renderer rend = cellIndicator.GetComponentInChildren<Renderer>();
        
        if (rend != null)
        {
            float halfHeight = rend.bounds.size.y / 2f;
            cellWorldPos.y += halfHeight;
        }

        cellIndicator.transform.position = cellWorldPos;

        if (selectObjectIndex < 0)
            return;

        if (_isTopSpawning) 
        {
            bool placementValidity = IsTopSpawnning();
            previewRenderer.material.color = placementValidity ? Color.white : Color.red;   
        }
        else
        {
            bool placementValidity = CheckPlacementValidity(gridPosition, selectObjectIndex);
            previewRenderer.material.color = placementValidity ? Color.white : Color.red;   
        }
    }
}
