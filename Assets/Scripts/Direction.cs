using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Direction {
    public const int NORTH = 0;
    public const int EAST = 1;
    public const int SOUTH = 2;
    public const int WEST = 3;
    
    public static int GetOppositeDirection(int dir)
    {
        return (dir + 2) % 4;
    }

    internal static int GetHorizonalComp(int direction)
    {
        switch (direction)
        {
            case EAST:
                return 1;
            case WEST:
                return -1;
            default:
                return 0;
        }
    }

    internal static int GetVerticalComp(int direction)
    {
        switch (direction)
        {
            case NORTH:
                return 1;
            case SOUTH:
                return -1;
            default:
                return 0;
        }
    }

    internal static string GetDirectionalString(bool[] directions)
    {
        if (directions == null)
        {
            Debug.LogWarning("direction == null");
            return "0000";
        }
        String bitString = "";
        for (int i = 0; i < directions.Length; i++)
        {
            bitString += directions[i] ? "1" : "0";
        }
        return bitString;
    }
}
