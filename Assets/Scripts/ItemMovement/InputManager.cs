using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[RequireComponent(typeof(NeighbourManager))]
public class InputManager : MonoBehaviour
{
    public bool[] Status = new bool[4] { true, true, true, true };
    public IReceiver<Package<ItemStack>> Receiver;

    private NeighbourManager neighbourManager;

    private void Start()
    {
        neighbourManager = GetComponent<NeighbourManager>();
    }
    public void SetStatus(int dir, bool enabled)
    {
        Status[dir] = enabled;
        if(neighbourManager != null)
        {
            GetComponent<TileObject>().UpdateSprite();
            neighbourManager.NotifyMyNeighbour(dir);
        }
        
    }

    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < 4; i++)
        {
            stringBuilder.Append(Status[i] ? "1" : "0");
        }
        return stringBuilder.ToString();
    }

    internal void SetReceiver(IReceiver<Package<ItemStack>> receiver)
    {
        Receiver = receiver;
        GetComponent<NeighbourManager>().NotifyAllNeighbours();
    }

    internal bool HasReceiver(int dir)
    {
        if(Receiver == null)
        {
            return false;
        }
        return Status[dir];
        
    }
}
