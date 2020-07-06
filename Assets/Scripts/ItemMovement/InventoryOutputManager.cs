using System;
using UnityEngine;

[RequireComponent(typeof(OutputManager))]
[RequireComponent(typeof(InventoryManager))]
public class InventoryOutputManager : MonoBehaviour
{
    private Inventory inventory;
    private OutputManager outputManager;

    private Package<ItemStack> currentPackage;

    public float sendRate;
    public float cooldown;
    // Start is called before the first frame update
    void Start()
    {
        inventory = GetComponent<InventoryManager>().GetInventory(0);
        outputManager = GetComponent<OutputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        cooldown -= Time.deltaTime;
        if (cooldown <= 0)
        {
            cooldown = sendRate;
            if (currentPackage == null)
            {
                ItemStack itemStack = inventory.GetFirstStack();
                if(itemStack != null)
                {
                    currentPackage = new Package<ItemStack>(itemStack);
                }
                
            }
            if(currentPackage == null)
            {
                //WE STILL DONT HAVE ANY ITEMS TO SEND
                return;
            }
            if(currentPackage.Directions != null)
            {
                throw new NotImplementedException();
                //TODO send package in correct direction
            }
            
            for (int i = 0; i < 4; i++)
            {
                if (!outputManager.Request(i))
                {
                    continue;
                }
                bool sent = outputManager.Send(currentPackage, i);
                if(sent)
                {
                    currentPackage = null;
                    return;
                }
            }
        }
    }

    public override string ToString()
    {
        return currentPackage == null ? "empty" : currentPackage.ToString();
    }
}
