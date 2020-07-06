using System;
using System.Collections.Generic;
using System.Text;

public class Inventory
{
    public int Capacity;
    public List<ItemStack> items;

    public Inventory(int capacity)
    {
        Capacity = capacity;
        items = new List<ItemStack>(Capacity);
    }

    internal ItemStack GetFirstStack()
    {
        if (items.Count == 0) return null;
        return GetStackOf(items[0].Item);
    }

    //returns up to a stack of a partiular item
    internal ItemStack GetStackOf(Item item)
    {
        if (items.Count == 0)
        {
            return null;
        }
        //TODO only takes from one stack, need to fix if there is a stack of say 10 before a stack of 100, it should return 100
        ItemStack newStack = items.Find(stack => stack.Item.Name == item.Name);
        items.Remove(newStack);
        return newStack;
    }

    internal bool isFull()
    {
        //TODO how to check if inventory is full?
        return false;
    }

    public bool AddItemStack(ItemStack itemStack)
    {
        if (itemStack == null)
        {
            return true;
        }
        ItemStack currentStack = items.Find(stack => stack.Item.Name == itemStack.Item.Name && stack.Quantity < ItemStack.MAX_QUANTITY);
        while (currentStack != null && itemStack != null)
        {
            currentStack.Add(ref itemStack);

            if (itemStack == null || currentStack == null) break;

            currentStack = items.Find(stack => stack.Item.Name == itemStack.Item.Name && stack.Quantity < ItemStack.MAX_QUANTITY);
        }
        if (itemStack != null && items.Count >= Capacity)
        {
            return false;
        }
        if (itemStack != null)
        {
            //some items left but no place to put them... but them in a new stack
            items.Add(itemStack);
        }
        return true;
    }

    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        foreach(ItemStack stack in items)
        {
            stringBuilder.Append(stack.ToString());
        }
        return stringBuilder.ToString();
    }
}