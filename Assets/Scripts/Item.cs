using System;
using UnityEngine;

[Serializable]
public class Item
{

    [SerializeField]
    public string Name;


    public Item(String name)
    {
        this.Name = name;
    }

    public override string ToString()
    {
        return Name;
    }
}