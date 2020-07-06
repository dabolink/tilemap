using System;
using UnityEngine;

[RequireComponent(typeof(TileObject))]
[RequireComponent(typeof(NeighbourManager))]
public class OutputManager : MonoBehaviour
{
    public bool[] Status = new bool[4] { true, true, true, true };

    NeighbourManager neighbourManager;

    private void Start()
    {
        neighbourManager = GetComponent<NeighbourManager>();
        neighbourManager.NotifyAllNeighbours();

    }

    internal bool Send(Package<ItemStack> package, int dir)
    {
        IReceiver<Package<ItemStack>> receiver = GetReceiver(dir);
        if(receiver == null)
        {
            Debug.LogWarning("Send to null even though request was made???");
            return false;
        }
        return receiver.Receive(package, Direction.GetOppositeDirection(dir));

    }

    public void SetStatus(int dir, bool enabled)
    {
        Status[dir] = enabled;
        if (neighbourManager != null)
        {
            //needs check because of inspector changes
            GetComponent<TileObject>().UpdateSprite();
            neighbourManager.NotifyMyNeighbour(dir);
        }
    }

    internal bool Request(int dir)
    {
        IReceiver<Package<ItemStack>> receiver = GetReceiver(dir);
        if(receiver == null)
        {
            return false;
        }
        return receiver.ReceiveRequest(Direction.GetOppositeDirection(dir));
    }

    public bool HasSender(int direction)
    {
        return Status[direction];
    }

    private void OnValidate()
    {
        for (int i = 0; i < 4; i++)
        {
            SetStatus(i, Status[i]);
        }
    }

    private IReceiver<Package<ItemStack>> GetReceiver(int dir)
    {
        if (!Status[dir])
        {
            return null;
        }
        InputManager inputManager = neighbourManager.GetNeighbour<InputManager>(dir);
        if (inputManager == null)
        {
            return null;
        }

        IReceiver<Package<ItemStack>> receiver = inputManager.Receiver;
        if (inputManager.Receiver == null)
        {
            return null;
        }
        return receiver;
    }
}
