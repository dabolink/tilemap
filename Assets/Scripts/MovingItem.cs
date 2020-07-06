using UnityEngine;

public class MovingItem : MonoBehaviour
{
    private Vector3 _startLocation;
    public Vector3 StartLocation
    {
        get { return _startLocation; }
        set {
            _startLocation = value;
            transform.position = value;
            status = Status.MOVEMENT_1;
        }
    }
    private Vector3 centerLocation;

    private Vector3 _endLocation;
    public Vector3 EndLocation
    {
        get { return _endLocation; }
        set {
            _endLocation = value;
            status = Status.MOVEMENT_2;
        }
    }

    private Package<ItemStack> _package;
    private SpriteRenderer spriteRenderer;
    private Sprite Sprite { 
        get
        {
            return spriteRenderer.sprite;
        }
        set
        {
            spriteRenderer.sprite = value;
        }
    }

    public Package<ItemStack> Package { 
        get { return _package; } 
        set {
            _package = value;
            UpdateSprite();
        } 
    }

    private void UpdateSprite()
    {
        Sprite = Resources.Load<Sprite>("Item-Sprites/" + _package.Item.Item.Name);
    }

    public TileObject currentTO;
    public Status status = Status.INIT;

    public float MovementSpeed = 0f;

    private void OnEnable()
    {
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        currentTO = transform.GetComponentInParent<TileObject>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Vector3 tileWorldPos = currentTO.GetTileWorldPosition();
        centerLocation = new Vector3(tileWorldPos.x, tileWorldPos.y + 0.25f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 finalPosition;
        switch (status)
        {
            case Status.INIT:
            default:
                return;
            case Status.HALFWAY:
                currentTO.GetComponent<ItemPassthroughManager>().CurrentStatus = ItemPassthroughManager.Status.REQUEST_SEND;
                return;
            case Status.FINISHED:
                currentTO.GetComponent<ItemPassthroughManager>().CurrentStatus = ItemPassthroughManager.Status.SENDING;
                return;
            case Status.MOVEMENT_1:
                finalPosition = centerLocation;
                break;
            case Status.MOVEMENT_2:
                finalPosition = EndLocation;
                break;
        }
        if((finalPosition - transform.position).sqrMagnitude < 0.0001)
        {
            // we are at our next position
            status.GoNext();
        }
        transform.position = Vector3.MoveTowards(transform.position, finalPosition, Time.deltaTime * MovementSpeed);
    }
}
public enum Status
{
    INIT,
    MOVEMENT_1,
    HALFWAY,
    MOVEMENT_2,
    FINISHED,
}
public static class Extensions
{
    public static void GoNext(ref this Status status)
    {
        switch (status)
        {
            case Status.INIT:
                status = Status.MOVEMENT_1;
                return;
            case Status.MOVEMENT_1:
                status = Status.HALFWAY;
                return;
            case Status.HALFWAY:
                status = Status.MOVEMENT_2;
                return;
            case Status.MOVEMENT_2:
            case Status.FINISHED:
            default:
                status = Status.FINISHED;
                return;
        }
        
    }
}
