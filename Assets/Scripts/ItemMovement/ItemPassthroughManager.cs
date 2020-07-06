using System;
using UnityEngine;

[RequireComponent(typeof(OutputManager))]
[RequireComponent(typeof(InputManager))]
public class ItemPassthroughManager : MonoBehaviour, IReceiver<Package<ItemStack>>
{
    public MovingItem MovingItemPrefab;

    public enum Status {
        EMPTY,
        AWAITING_ITEM,
        MOVING_1,
        REQUEST_SEND,
        MOVING_2,
        SENDING,
    };
    
    private OutputManager outputManager;
    private InputManager inputManager;

    public Status CurrentStatus;


    MovingItem currentItem;
    public int receivingDir = -1;
    public int outputDirection = -1;
    public float MovementSpeed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        inputManager = GetComponent<InputManager>();
        outputManager = GetComponent<OutputManager>();
        inputManager.SetReceiver(this);
    }

    private void Update()
    {
        switch(CurrentStatus)
        {
            case Status.SENDING:
                DoSend();
                return;
            case Status.REQUEST_SEND:
                DoRequestSend();
                return;
        }
    }

    private void DoSend()
    {
        if (!outputManager.Request(outputDirection))
        {
            return;
        }
        bool completed = SendPackage();
        if (completed)
        {
            DestroyMovingItem();
        }
    }

    private bool SendPackage()
    {
        return outputManager.Send(currentItem.Package, outputDirection);
    }

    private void DoRequestSend()
    {
        if (currentItem.Package.Directions != null)
        {
            if(!outputManager.Request(currentItem.Package.Directions[0]))
            {
                Debug.LogError("Can't Send in direction told to???");
                return;
            }
            SetEndLocationOfPackage(currentItem.Package.Directions[0]);
        }
        else
        {
            // if no directions just pass on to next receiver
            for (int i = 0; i < 4; i++)
            {
                //DON'T SEND BACKWARDS
                if (i != receivingDir && outputManager.Request(i))
                {
                    SetEndLocationOfPackage(i);
                    break;
                }

            }
        }
    }

    private void SetEndLocationOfPackage(int dir)
    {
        // IF WE GET HERE WE ARE ALLOWED TO SEND THE PACKAGE IN THIS DIRECTION
        outputDirection = dir;
        currentItem.EndLocation = GetComponent<TileObject>().GetPointOnTileSide(dir);
        CurrentStatus = Status.MOVING_2;
    }

    public bool Receive(Package<ItemStack> package, int dir)
    {
        if (IsBusy())
        {
            return false;
        }
        if (CurrentStatus != Status.EMPTY && CurrentStatus != Status.AWAITING_ITEM)
        {
            Debug.LogWarning("Receive attempt made after another direction requested already :: " + outputDirection  + " :: " + dir);
            return false;
        }
        InitMovingItem(package, dir);
        return true;
    }

    private void InitMovingItem(Package<ItemStack> package, int dir)
    {
        outputDirection = -1;
        receivingDir = dir;
        currentItem = Instantiate(MovingItemPrefab, transform);
        currentItem.Package = package;
        currentItem.StartLocation = GetComponent<TileObject>().GetPointOnTileSide(dir);
        currentItem.MovementSpeed = MovementSpeed;
        CurrentStatus = Status.MOVING_1;
    }

    private void DestroyMovingItem()
    {
        Destroy(currentItem.gameObject);
        currentItem = null;
        CurrentStatus = Status.EMPTY;
        outputDirection = -1;
        receivingDir = -1;
    }

    public override string ToString()
    {
        string packageString = currentItem == null ? "empty" : currentItem.ToString();
        string statusString = CurrentStatus.ToString();
        string managerString = inputManager == null ? "empty" : inputManager.ToString();
        return String.Format("{0}\n{1}\n", packageString, statusString, managerString);
    }

    public bool ReceiveRequest(int dir)
    {
        if (IsBusy())
        {
            return false;
        }
        if(receivingDir != dir && receivingDir != -1)
        {
            return false;
        }
        receivingDir = dir;
        CurrentStatus = Status.AWAITING_ITEM;
        return true;
    }

    private bool IsBusy()
    {
        return currentItem != null && CurrentStatus != Status.EMPTY && CurrentStatus != Status.AWAITING_ITEM;
    }

    private void OnDestroy()
    {
        if(currentItem != null)
        {
            Destroy(currentItem.gameObject);
        }
        
    }
}
