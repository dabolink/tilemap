using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageTileObjects : MonoBehaviour
{
    TileObject[,] tileObjects;
    ManageMap mapManager;
    // Start is called before the first frame update
    void OnEnable()
    {
        mapManager = FindObjectOfType<ManageMap>();
        tileObjects = new TileObject[mapManager.width, mapManager.height];
    }

    internal TileObject[] GetTileNeighbours(Vector3Int position)
    {
        TileObject[] neighbours = new TileObject[4];
        if (position.y + 1 < tileObjects.GetLength(1))
        {
            neighbours[0] = tileObjects[position.x, position.y + 1];
        }
        if (position.x + 1 < tileObjects.GetLength(0))
        {
            neighbours[1] = tileObjects[position.x + 1, position.y];
        }
        if (position.y - 1 >= 0)
        {
            neighbours[2] = tileObjects[position.x, position.y - 1];
        }
        if (position.x - 1 >= 0)
        {
            neighbours[3] = tileObjects[position.x - 1, position.y];
        }
        return neighbours;
    }

    internal T[] GetTileNeighbours<T>(Vector3Int position)
    {
        T[] neighbours = new T[4];

        neighbours[0] = GetObjectAt<T>(position.x, position.y + 1);
        neighbours[1] = GetObjectAt<T>(position.x + 1, position.y);
        neighbours[2] = GetObjectAt<T>(position.x, position.y - 1);
        neighbours[3] = GetObjectAt<T>(position.x - 1, position.y);

        return neighbours;
    }

    public T GetObjectAt<T>(int x, int y)
    {
        TileObject tileObject = GetObjectAt(new Vector3Int(x, y, 0));
        if(tileObject == null)
        {
            return default;
        }
        return tileObject.GetComponent<T>();
    }

    internal TileObject GetTileNeighbour(int x, int y, int direction)
    {
        x += Direction.GetHorizonalComp(direction);
        y += Direction.GetVerticalComp(direction);
        Vector3Int location = new Vector3Int(x, y, 0);
        if (!isValidTile(location))
        {
            return null;
        }
        return GetObjectAt(location);
    }

    internal TileObject GetObjectAt(Vector3Int localPosition)
    {
        if (!isValidTile(localPosition))
        {
            return null;
        }
        return tileObjects[localPosition.x, localPosition.y];
    }

    internal bool AddBuilding(TileObject buildingPrefab, Vector3Int tilePosition)
    {
        if (!isValidTile(tilePosition))
        {
            return false;
        }
        if (tileObjects[tilePosition.x, tilePosition.y] != null)
        {
            return false;
        }
        TileObject go = Instantiate(buildingPrefab);
        go.Data = new TileObjectData(tilePosition);
        go.transform.position = mapManager.TileToWorld(tilePosition);
        tileObjects[tilePosition.x, tilePosition.y] = go;
        return true;
    }

    private bool isValidTile(Vector3Int tilePosition)
    {
        return tilePosition.x >= 0 && tilePosition.y >= 0 && tilePosition.x < tileObjects.GetLength(0) && tilePosition.y < tileObjects.GetLength(1);
    }

    internal bool RemoveBuilding(Vector3Int tilePosition)
    {
        if (!isValidTile(tilePosition))
        {
            return false;
        }
        if (tileObjects[tilePosition.x, tilePosition.y] == null)
        {
            return false;
        }
        TileObject building = GetObjectAt(tilePosition);
        Destroy(building.gameObject);
        tileObjects[tilePosition.x, tilePosition.y] = null;
        return true;
    }
}
