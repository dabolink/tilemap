using System;
using System.Collections;

[Serializable]
public class ItemStack : System.IComparable<ItemStack>
{
    public static int MAX_QUANTITY = 100;

    public int Quantity { protected set; get; }
    
    public Item Item { protected set; get; }

    public ItemStack(int quantity, Item item)
    {
        this.Quantity = quantity;
        this.Item = item;
    }

    public bool Add(ref ItemStack newStack)
    {
        if(Item.Name != newStack.Item.Name)
        {
            //can't combine stacks of different types
            return false;
        }
        int totalQuantity = Quantity + newStack.Quantity;
        if(totalQuantity > MAX_QUANTITY)
        {
            this.Quantity = MAX_QUANTITY;
            newStack.Quantity = totalQuantity - MAX_QUANTITY;
        }
        else
        {
            this.Quantity = totalQuantity;
            newStack = null;
        }
        return true;
    }

    public override string ToString()
    {
        return "Quantity: " + Quantity + " :: " + "Item:" + Item.ToString();
    }

    public int CompareTo(ItemStack other)
    {
        return other.Item.Name == Item.Name ? 0 : 1;
    }
}