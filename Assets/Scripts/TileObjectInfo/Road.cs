using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NeighbourManager))]
[RequireComponent(typeof(ItemPassthroughManager))]
public class Road : MonoBehaviour, ITileObjInfo
{
    ItemPassthroughManager itemManager;
    public override string ToString()
    {
        string directionalString = GetComponent<NeighbourManager>().GetDirectionString() ?? "_ _ _ _";
        string itemPassthroughString = itemManager == null ? "null itemManager" : itemManager.ToString();
        return string.Format("{0}: {1}\n {2}", "Road:", directionalString, itemPassthroughString);
    }

    // Start is called before the first frame update
    void Start()
    {
        itemManager = GetComponent<ItemPassthroughManager>();
    }
}
