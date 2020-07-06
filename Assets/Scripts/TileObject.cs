using System;
using UnityEngine;

[RequireComponent(typeof(NeighbourManager))]
public class TileObject : MonoBehaviour
{
    public TileObjectData Data {get; set;}
    NeighbourManager neighbourManager;
    IUpdateSprite spriteUpdater;

    public void OnEnable()
    {
        neighbourManager = GetComponent<NeighbourManager>();
        spriteUpdater = GetComponent<IUpdateSprite>();
    }

    public void OnDestroy()
    {
        neighbourManager.NotifyAllNeighbours();
    }

    public override string ToString()
    {
        return String.Format("Position {0},{1} and Size {2},{3}", Data.X, Data.Y, Data.Width, Data.Height);
    }

    internal void UpdateSprite()
    {
        spriteUpdater.UpdateSprite();
    }

    internal Vector3 GetPointOnTileSide(int direction)
    {
        float x = transform.position.x, y = transform.position.y + 0.25f;
        switch (direction)
        {
            case Direction.NORTH:
                x += -0.25f;
                y += 0.125f;
                break;
            case Direction.EAST:
                x += 0.25f;
                y += 0.125f;
                break;
            case Direction.SOUTH:
                x += 0.25f;
                y += -0.125f;
                break;
            case Direction.WEST:
                x += -0.25f;
                y += -0.125f;
                break;

            default:
                Debug.LogWarning("Direction value ::" + direction + "isn't standardized");
                break;

        }
        return new Vector3(x, y);
    }
    internal Vector3 GetTileWorldPosition()
    {
        return transform.position;
    }
}
