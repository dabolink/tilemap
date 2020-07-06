using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParcelManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public List<int> GetDirections(Vector3Int startPos, Vector3Int endPos)
    {
        //TODO: make smart
        List<int> directions = new List<int>();
        Vector3Int currentPos = new Vector3Int(startPos.x, startPos.y, 0);
        while (currentPos.x != endPos.x && currentPos.y != endPos.y) {
            if (currentPos.x < endPos.x)
            {
                directions.Add(1);
                currentPos.x++;
            }
            else if (currentPos.x > endPos.x)
            {
                directions.Add(3);
                currentPos.x--;
            }
            else if (currentPos.y < endPos.y)
            {
                directions.Add(0);
                currentPos.y++;
            }
            else if (currentPos.y > endPos.y)
            {
                directions.Add(2);
                currentPos.y--;
            }
        }
        return directions;
        
    }
}
