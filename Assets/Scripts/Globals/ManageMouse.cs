using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;
using UnityEngine.EventSystems;

public class ManageMouse : MonoBehaviour
{
    public GameObject selectorPrefab;
    public TileObject[] tileObjectPrefabs;
    public int CurrentObjectIndex = 0;

    ManageMap mapManager;
    ManageUI UIManager;
    ManageTileObjects tileObjectManager;
    TileObjectUIManager tileObjectUIManager;

    GameObject selector;


    // Start is called before the first frame update
    void Start()
    {
        mapManager = FindObjectOfType<ManageMap>();
        UIManager = FindObjectOfType<ManageUI>();
        tileObjectManager = FindObjectOfType<ManageTileObjects>();
        tileObjectUIManager = tileObjectManager.GetComponent<TileObjectUIManager>();
        selector = Instantiate(selectorPrefab);
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;

        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(mousePos);

        Vector3Int tilePosition = mapManager.WorldToTilePosition(worldPoint);
        TileBase tile = mapManager.GetTileAt(tilePosition);
        if (tile == null)
        {
            selector.SetActive(false);
        }
        else
        {
            selector.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            CurrentObjectIndex = (CurrentObjectIndex + 1) % tileObjectPrefabs.Length;
        } 
        else if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            CurrentObjectIndex = CurrentObjectIndex <= 0 ? tileObjectPrefabs.Length - 1 : CurrentObjectIndex - 1;
        }

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (Input.GetMouseButton(0))
        {
            if(!tileObjectManager.AddBuilding(tileObjectPrefabs[CurrentObjectIndex], tilePosition))
            {
                //for debug purposes highlight the current object in the inspector
                TileObject currentObject = tileObjectManager.GetObjectAt(tilePosition);
                UIManager.UpdateInfoBox(tileObjectManager.GetObjectAt<ITileObjInfo>(tilePosition.x, tilePosition.y));
                if (currentObject != null)
                {
                    Selection.activeObject = currentObject;
                    tileObjectUIManager.SetCurrentTO(currentObject);
                }
                else
                {
                    tileObjectUIManager.SetCurrentTO(null);
                }
            }
        }
        if (Input.GetMouseButton(1))
        {
            tileObjectManager.RemoveBuilding(tilePosition);
            tileObjectUIManager.SetCurrentTO(null);
        }
        selector.transform.position = mapManager.TileToWorld(tilePosition);
        UIManager.UpdateCurrentTile(tilePosition, worldPoint);
        UIManager.UpdatePrefabInfo(tileObjectPrefabs[CurrentObjectIndex]);
        

    }
}
