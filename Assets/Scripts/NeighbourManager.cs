using System;
using UnityEngine;

public class NeighbourManager : MonoBehaviour
{
    public bool[] direction = new bool[4];

    private ManageTileObjects tileObjectManager;
    private TileObject tileObject;


    private void OnEnable()
    {
        tileObjectManager = FindObjectOfType<ManageTileObjects>();
        tileObject = GetComponent<TileObject>();
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateNeighbourDirections();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetDirectionString()
    {
        return Direction.GetDirectionalString(GetConnections());
    }

    private bool[] GetConnections()
    {
        bool[] connections = new bool[4];
        for (int i = 0; i < 4; i++)
        {
            connections[i] = HasConnection(i);
        }
        return connections;
    }

    internal T GetNeighbour<T>(int direction)
    {
        
        TileObject neighbour = tileObjectManager.GetTileNeighbour(tileObject.Data.X, tileObject.Data.Y, direction);
        if (neighbour == null)
        {
            return default;
        }
        return neighbour.GetComponent<T>();
    }

    private void UpdateNeighbourDirection(int dir)
    {
        direction[dir] = HasConnection(dir);
    }

    private bool HasConnection(int dir)
    {
        OutputManager[] neighboursSenders = GetNeighbouringObjects<OutputManager>();
        InputManager[] neighboursReceivers = GetNeighbouringObjects<InputManager>();
        bool neighbourHasASender = neighboursSenders != null && neighboursSenders[dir] != null && neighboursSenders[dir].HasSender(Direction.GetOppositeDirection(dir));
        bool neighbourHasAReceiver = neighboursReceivers != null && neighboursReceivers[dir] != null && neighboursReceivers[dir].HasReceiver(Direction.GetOppositeDirection(dir));
        bool hasSender = GetComponent<OutputManager>() != null && GetComponent<OutputManager>().Status[dir];
        bool hasReceiver = GetComponent<InputManager>() != null && GetComponent<InputManager>().Status[dir];
        return ((neighbourHasAReceiver && hasSender) || (neighbourHasASender && hasReceiver));
    }

    internal void NotifyMyNeighbour(int dir)
    {
        NeighbourManager neighbour = GetNeighbour<NeighbourManager>(dir);
        if (neighbour != null)
        {
            neighbour.NotifyNeighbourUpdated(Direction.GetOppositeDirection(dir));
        }
    }

    public void NotifyNeighbourUpdated(int dir)
    {
        UpdateNeighbourDirection(dir);
        tileObject.UpdateSprite();
    }

    public TileObject[] GetNeighbouringObjects()
    {
        return tileObjectManager.GetTileNeighbours(new Vector3Int(tileObject.Data.X, tileObject.Data.Y, 0));
    }

    private void UpdateNeighbourDirections()
    {
        for (int i = 0; i < 4; i++)
        {
            direction[i] = HasConnection(i);
        }
    }

    internal T[] GetNeighbouringObjects<T>() where T : MonoBehaviour
    {
        if(tileObjectManager == null)
        {
            return new T[4];
        }

        T[] neighbours = tileObjectManager.GetTileNeighbours<T>(new Vector3Int(tileObject.Data.X, tileObject.Data.Y, 0));

        for (int i = 0; i < neighbours.Length; i++)
        {
            if (neighbours[i] == null || neighbours[i].GetComponent<T>() == null)
            {
                neighbours[i] = null;
            }
        }
        return neighbours;
    }

    public void NotifyAllNeighbours()
    {
        for (int i = 0; i < 4; i++)
        {
            NotifyMyNeighbour(i);
        }
    }
}
