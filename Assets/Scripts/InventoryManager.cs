using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<int> InventorySizes = new List<int>();
    public List<Inventory> Inventories = new List<Inventory>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < InventorySizes.Count; i++)
        {
            Inventories.Add(new Inventory(InventorySizes[i]));
        }
    }

    public override string ToString()
    {
        if(Inventories.Count == 0)
        {
            return "empty";
        }
        string inventoryString = "";
        foreach (Inventory inventory in Inventories)
        {
            inventoryString += inventory.ToString() + "\n";
        }
        return inventoryString;
    }

    public Inventory GetInventory(int i)
    {
        return Inventories[i];
    }
}
