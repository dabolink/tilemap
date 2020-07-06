using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OutputManager))]
[RequireComponent(typeof(InventoryOutputManager))]
[RequireComponent(typeof(InventoryInputManager))]
[RequireComponent(typeof(TileObject))]
[RequireComponent(typeof(NeighbourManager))]
[RequireComponent(typeof(InventoryManager))]
public class Warehouse : MonoBehaviour, ITileObjInfo
{
    private InventoryOutputManager invOutputManager;
    private InventoryManager inventoryManager;

    // Start is called before the first frame update
    void Start()
    {
        invOutputManager = GetComponent<InventoryOutputManager>();
        inventoryManager = GetComponent<InventoryManager>();
    }

    public override string ToString()
    {
        if(inventoryManager == null || invOutputManager == null)
        {
            return "Warehouse: loading...";
        }
        return string.Format("Warehouse:\n {0}\n{1}", inventoryManager.ToString(), invOutputManager);
    }


}
