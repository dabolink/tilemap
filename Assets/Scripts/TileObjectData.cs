using UnityEngine;

public class TileObjectData
{
    public int X { protected set; get; }
    public int Y { protected set; get; }
    public int Width { protected set; get; }
    public int Height { protected set; get; }

    public TileObjectData(Vector3Int tilePosition, int width=1, int height=1)
    {
        this.X = tilePosition.x;
        this.Y = tilePosition.y;
        this.Width = width;
        this.Height = height;
    }
}