using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageUI : MonoBehaviour
{
    public Text prefabTextBox;
    public Text infoTextBox;
    public Text tilePositionTextBox;

    public void UpdateCurrentTile(Vector3Int tilePosition, Vector3 worldPoint)
    {
        tilePositionTextBox.text = string.Format("{0},{1}\n{2},{3}", tilePosition.x, tilePosition.y, worldPoint.x, worldPoint.y);
    }

    public void UpdateInfoBox(ITileObjInfo obj)
    {
        if (obj != null)
        {
            infoTextBox.text = obj.ToString();
        } else
        {
            infoTextBox.text = "";
        }
    }

    internal void UpdatePrefabInfo(TileObject tileObject)
    {
        prefabTextBox.text = tileObject.name;
    }
}
