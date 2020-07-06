using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NeighbourManager))]
[RequireComponent(typeof(InventoryManager))]
[RequireComponent(typeof(InventoryOutputManager))]
[RequireComponent(typeof(Generator))]
public class Farm : MonoBehaviour, ITileObjInfo
{
    private InventoryOutputManager inventoryOutputManager;
    private InventoryManager inventoryManager;

    public override string ToString()
    {
        if(inventoryManager == null)
        {
            return "Farm:\n" + "loading...";
        }
        return "Farm:\n" + inventoryManager.ToString() + "\n" + inventoryOutputManager.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        inventoryManager = GetComponent<InventoryManager>();
        inventoryOutputManager = GetComponent<InventoryOutputManager>();
    }
}
