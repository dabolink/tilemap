using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InventoryManager))]
[RequireComponent(typeof(InputManager))]
public class InventoryInputManager : MonoBehaviour, IReceiver<Package<ItemStack>>
{
    private InventoryManager inventoryManager;
    private InputManager inputManager;
    public bool Receive(Package<ItemStack> package)
    {
        return inventoryManager.GetInventory(0).AddItemStack(package.Item);
    }

    public bool Receive(Package<ItemStack> package, int direction)
    {
        return Receive(package);
    }

    public bool ReceiveRequest(int dir)
    {
        return !inventoryManager.GetInventory(0).isFull();
    }

    // Start is called before the first frame update
    void Start()
    {
        inputManager = GetComponent<InputManager>();
        inventoryManager = GetComponent<InventoryManager>();
        inputManager.SetReceiver(this);
    }
}
