using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridData
{
    Dictionary<Vector3Int, PlacementData> placedObjects = new();

    public void AddObjectAt(Vector3Int gridPosition, Vector2Int objectSize, int ID, int placedObjectIndex)
    {
        List<Vector3Int> posistionsToOccuPy = CalculatePosistions(gridPosition, objectSize);
        PlacementData data = new PlacementData(posistionsToOccuPy, ID, placedObjectIndex);
        foreach (var pos in posistionsToOccuPy)
        {
            if (placedObjects.ContainsKey(pos))
            {
                throw new Exception($"Dictionary already contains this cell posistion");
            }
            placedObjects[pos] = data;
        }
    }

    private List<Vector3Int> CalculatePosistions(Vector3Int gridPosition, Vector2Int objectSize)
    {
        List<Vector3Int> returnVal = new();
        for (int x = 0; x < objectSize.x; x++)
        {
            for (int y = 0; y < objectSize.y; y++)
            {
                returnVal.Add(gridPosition + new Vector3Int(x,0,y));
            }
        }
        return returnVal;
    }
    public bool CanPlaceObjectAt(Vector3Int gridPosition, Vector2Int objectSize)
    {
        List<Vector3Int> posistionToOccupy = CalculatePosistions(gridPosition,objectSize);
        foreach(var pos in posistionToOccupy)
        {
            if(placedObjects.ContainsKey(pos))
            {
                return false;
            }
        }
        return true;
    }
}
public class PlacementData
{
    public List<Vector3Int> occupiedPosistions;
    public int ID { get; private set; }
    public int PlacedObjectsIndex { get; private set; }

    public PlacementData(List<Vector3Int> occupiedPosistions, int iD, int placedObjectsIndex)
    {
        this.occupiedPosistions = occupiedPosistions;
        ID = iD;
        PlacedObjectsIndex = placedObjectsIndex;
    }
}
