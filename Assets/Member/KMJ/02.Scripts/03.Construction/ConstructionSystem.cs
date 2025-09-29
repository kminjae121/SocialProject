using System;
using System.Collections.Generic;
using Core.Events;
using Member.LCM._01.Scripts.UI;
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
    [SerializeField] private LayerMask _cantConstruction;
    [SerializeField] private GameEventChannelSO _mapChannel;
    
    [SerializeField] private GameObject loadingUI;
    [SerializeField] private Transform uiRoot;

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
        StopPlaceMent();
        _mapChannel.RaiseEvent(MapEvents.GridMaterialEvent.Initialize(true));
        _isSpawning = true;
        
        selectObjectIndex = database.objectData.FindIndex(data =>
            data.ID == ID);

        
        
        if (database.objectData[selectObjectIndex].price > ResourceManager.Instance.Money)
        {
            return;
        }        
        
        if (database.objectData[selectObjectIndex].Name == "SunFactory")
        {
            ConstructTopFactory();
            return;
        }
        if (selectObjectIndex < 0)
        {
            return;
        }
        
        gridVisualization.SetActive(true);
        
        GameObject cellChanger = Instantiate(database.objectData[selectObjectIndex].visualPrefab);
        
        cellIndicator = cellChanger;
        
        previewRenderer = cellIndicator.GetComponentInChildren<Renderer>();
        
        cellIndicator.SetActive(true);
        _getMousePos.OnClicked += PlaceStructure;
        _getMousePos.OnExit += StopPlaceMent;
    }

    private void ConstructTopFactory()
    {
        _isTopSpawning = true;
        gridVisualization.SetActive(true);
        
        GameObject cellChanger = Instantiate(database.objectData[selectObjectIndex].visualPrefab);
        
        cellIndicator = cellChanger;
        
        previewRenderer = cellIndicator.GetComponentInChildren<Renderer>();
        
        cellIndicator.SetActive(true);
        
        _getMousePos.OnClicked += PlaceTop;
        _getMousePos.OnExit += StopPlaceMent;
    }
    
    private void PlaceTop()
    {
        if (_getMousePos.IsPointerOverUI())
            return;


        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        //if (Physics.Raycast(ray, out RaycastHit cantHit, 100f, _cantConstruction))
        //{
        //    return; 
        //}

        if (ResourceManager.Instance.CanConstructionObject(database.objectData[selectObjectIndex].price))
        {
            ResourceManager.Instance.ReduceSatisfaction(database.objectData[selectObjectIndex].price);
        }
        else
            return;

        Vector3 mousePosition = _getMousePos.GetWorldPosition();
        Vector3Int gridPosition = _grid.WorldToCell(mousePosition);

        bool placementValidity = CheckTopPlacementValidity(gridPosition, selectObjectIndex);
        
        if (placementValidity == false)
            return;

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            GameObject gameObj = Instantiate(database.objectData[selectObjectIndex].prefab);
            gameObj.transform.position = previewRenderer.transform.position;

            placeGameObjects.Add(gameObj);

            placeData.AddObjectAtTop(gridPosition,
                database.objectData[selectObjectIndex].size,
                database.objectData[selectObjectIndex].ID,
                placeGameObjects.Count - 1);

            gameObj.GetComponent<ConstructionObject>().StartConstructionObject();
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
        if (ResourceManager.Instance.CanConstructionObject(database.objectData[selectObjectIndex].price))
        {
            ResourceManager.Instance.ReduceSatisfaction(database.objectData[selectObjectIndex].price);
        }
        else
            return;
        
        Destroy(cellIndicator);
        
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


        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, int.MaxValue, _cantConstruction))
        {
            return; 
        }

        if (ResourceManager.Instance.CanConstructionObject(database.objectData[selectObjectIndex].price))
        {
            ResourceManager.Instance.ReduceSatisfaction(database.objectData[selectObjectIndex].price);
        }
        else
            return;

        Vector3 mousePosition = _getMousePos.GetWorldPosition();
        Vector3Int gridPosition = _grid.WorldToCell(mousePosition);
        
        LoadingUI _loadingUI = Instantiate(loadingUI, uiRoot).GetComponent<LoadingUI>();
        _loadingUI.SetPosition(gridPosition);
        _loadingUI.SetTimeAndStartLoading(5f);

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
    
    private bool CheckTopPlacementValidity(Vector3Int gridPosition, int selectObjectIndex)
    {
        GridData selectData = placeData;

        return selectData.CanPlaceObjectTop(gridPosition, database.objectData[selectObjectIndex].size);
    }
    

    private void StopPlaceMent()
    {
        _mapChannel.RaiseEvent(MapEvents.GridMaterialEvent.Initialize(false));
        _isSpawning = false;
        _isTopSpawning = false;
        selectObjectIndex = -1;
        gridVisualization.SetActive(false);
        //cellIndicator.SetActive(false);
        Destroy(cellIndicator);
        _getMousePos.OnClicked -= PlaceStructure;
        _getMousePos.OnExit -= StopPlaceMent;
    }
    
    private bool TryGetHighestStructorTopY(Vector3Int gridPosition, out float highestY)
    {
        highestY = 0f;
        Vector3 cellWorldPos = _grid.CellToWorld(gridPosition);

        float verticalHalfExtents = 5f;
        Vector3 boxCenter = cellWorldPos + Vector3.up * verticalHalfExtents;
        Vector3 halfExtents = new Vector3(_grid.cellSize.x / 2f, verticalHalfExtents, _grid.cellSize.z / 2f);

        Collider[] cols = Physics.OverlapBox(boxCenter, halfExtents, Quaternion.identity, _whatIsStructor);

        if (cols == null || cols.Length == 0)
            return false;

        float maxY = float.NegativeInfinity;
        foreach (var col in cols)
        {
            if (col == null) continue;
            if (col.gameObject == cellIndicator) continue;

            Renderer r = col.GetComponentInChildren<Renderer>();
            if (r != null)
                maxY = Mathf.Max(maxY, r.bounds.max.y);
            else
                maxY = Mathf.Max(maxY, col.bounds.max.y);
        }

        if (float.IsNegativeInfinity(maxY))
            return false;

        highestY = maxY;
        return true;
    }

    private void Update()
    {
        Vector3 mousePosition = _getMousePos.GetWorldPosition();
        Vector3Int gridPosition = _grid.WorldToCell(mousePosition);

        mouseIndicator.transform.position = mousePosition;

        Vector3 cellWorldPos = _grid.CellToWorld(gridPosition);

        if (cellIndicator == null)
            return;

        Renderer rend = cellIndicator.GetComponentInChildren<Renderer>();
        float previewHalfHeight = 0f;
        if (rend != null)
        {
            previewHalfHeight = rend.bounds.size.y / 2f;
        }

        float targetY = cellWorldPos.y + previewHalfHeight;

        if (TryGetHighestStructorTopY(gridPosition, out float highestY))
        {
            targetY = highestY + previewHalfHeight;
        }

        Vector3 finalPos = cellWorldPos;
        finalPos.y = Mathf.Lerp(cellIndicator.transform.position.y, targetY, 0.5f);
        cellIndicator.transform.position = finalPos;

        if (selectObjectIndex < 0)
            return;
        
        bool placementValidity;
        if (_isTopSpawning)
        {
            placementValidity = CheckTopPlacementValidity(gridPosition, selectObjectIndex);
        }
        else
        {
            placementValidity = CheckPlacementValidity(gridPosition, selectObjectIndex);
        }
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit,int.MaxValue, _cantConstruction))
        {
            placementValidity = false;
        }

        previewRenderer.material.color = placementValidity ? Color.white : Color.red;
    }

}
