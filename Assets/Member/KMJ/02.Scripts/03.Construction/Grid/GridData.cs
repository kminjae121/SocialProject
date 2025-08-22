using System;
using System.Collections.Generic;
using UnityEngine;

public class GridData : MonoBehaviour
{
    private Dictionary<Vector3Int, PlacementData> placeObjects = new();

    public void AddObjectAt(Vector3Int gridPosition, Vector2Int objectSize,
        int ID, int placeObjectIdx)
    {
        List<Vector3Int> positionToOccupy = CalculatePosition(gridPosition, objectSize);
        PlacementData data = new PlacementData(positionToOccupy, ID, placeObjectIdx);
        foreach (var placeObj in positionToOccupy)
        {
            if (placeObjects.ContainsKey(placeObj))
                throw new Exception($"Dictionarty already contains this cell position");

            placeObjects[placeObj] = data;
        }
    }

    private List<Vector3Int> CalculatePosition(Vector3Int gridPosition, Vector2Int objectSize)
    {
        List<Vector3Int> returnVal = new List<Vector3Int>();

        for (int x = 0; x < objectSize.x; x++)
        {
            for (int y = 0; y < objectSize.y; y++)
            {
                returnVal.Add(gridPosition + new Vector3Int(x, 0, y));
            }
        }

        return returnVal;
    }
    
    public bool CanPlaceObjectAt(Vector3Int gridPosition, Vector2Int objectSize)
    {
        List<Vector3Int> positionToOccupy = CalculatePosition(gridPosition, objectSize);
        foreach (var pos in positionToOccupy)
        {
            if (placeObjects.ContainsKey(pos))
                return false;
        }

        return true;
    }
}


public class PlacementData
{
    public List<Vector3Int> occupiedPositions;
    public int ID { get; private set; }
    public int PlaceObjectIndex { get; private set; }

    public PlacementData(List<Vector3Int> occupiedPositions, int ID, int PlaceObjectIndex)
    {
        this.occupiedPositions = occupiedPositions;
        this.ID = ID;
        this.PlaceObjectIndex = PlaceObjectIndex;
    }
}
    
