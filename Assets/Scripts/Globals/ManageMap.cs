using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ManageMap : MonoBehaviour
{
    public int width = 10;
    public int height = 10;

    public Tilemap tileMap;
    public TileBase ground;

    Grid grid;


    // Start is called before the first frame update
    void Start()
    {
        grid = tileMap.layoutGrid;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                tileMap.SetTile(new Vector3Int(x, y, 0), ground);
            }
        }
    }

    internal Vector3Int WorldToTilePosition(Vector3 worldPoint)
    {
        return grid.WorldToCell(worldPoint);

    }

    internal TileBase GetTileAt(Vector3Int localPosition)
    {
        return tileMap.GetTile(localPosition);
    }

    internal Vector3 TileToWorld(Vector3Int tileLocation)
    {
        return grid.CellToWorld(tileLocation);
    }
}
