using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Package<T> : System.IComparable<Package<T>> where T : System.IComparable<T>
{
    public T Item;
    public List<int> Directions { get; protected set; }

    public Package(T item, List<int> directions = default)
    {
        this.Item = item;
        this.Directions = directions;
    }

    public void UpdateDirections(List<int> directions)
    {
        this.Directions = directions;
    }

    public override string ToString()
    {
        return Item == null ?  "empty" : Item.ToString();
    }

    public int CompareTo(Package<T> other)
    {
        return other.CompareTo(this);
    }
}
