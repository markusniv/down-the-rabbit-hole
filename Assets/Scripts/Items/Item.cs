using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Abtract base class for all items.
/// </summary>
public abstract class Item : MonoBehaviour
{
    /// <summary>
    /// Icon used in UI (Inventory and hotbar)
    /// </summary>
    [SerializeField] private Sprite _icon = null;

    public Sprite Icon => _icon;

    /// <summary>
    /// This is reference to the Character who dropped this item. This is used to prevent item pickup immediately after dropping it.
    /// </summary>
    public Character RecentlyDroppedBy;

    /// <summary>
    /// Items value in some currency
    /// </summary>
    public int Value;

    /// <summary>
    /// MouseOver tracking boolean. When can disable functionality if mouse is over this item, such as attacking
    /// </summary>
    public bool MouseOver;

    #region Components

    private SpriteRenderer _spriteRenderer;

    public SpriteRenderer SpriteRenderer
    {
        get
        {
            if (_spriteRenderer == null) _spriteRenderer = GetComponent<SpriteRenderer>();
            return _spriteRenderer;
        }
    }

    #endregion Components

    /// <summary>
    /// Reference to the Inventory where this item is stored. This value is null if item is on the ground.
    /// </summary>
    public Inventory Inventory { get; set; }

    /// <summary>
    /// Tooltip that is shown when mouse is over this item in the UI
    /// </summary>
    public virtual string Tooltip { get; } = "Placeholder tooltip";

    protected virtual void FixedUpdate()
    {
    }

    /// <summary>
    /// This is called when this item is dropped from inventory
    /// </summary>
    /// <param name="droppedBy">Who dropped this item</param>
    public virtual void OnDrop(Character droppedBy) { }

    /// <summary>
    /// This is called when this item is added to inventory.
    /// </summary>
    /// <param name="pickedUpBy">Who picked this item</param>
    public virtual void OnPickup(Character pickedUpBy)
    {
        if (this is Consumable consumable && consumable.Thrown) return;
        if (pickedUpBy is Player)
        {
            SoundManagerScript.PlaySound(SoundManagerScript.Sound.Pickup);
        }
    }

    /// <summary>
    /// Pick up the item when entering its collider if the item wasn't recently dropped.
    /// </summary>
    /// <param name="col">The character who enters the item's collider</param>
    public virtual void OnTriggerEnter2D(Collider2D col)
    {
    }

    /// <summary>
    /// When leaving the item collider after dropping an item, change RecentlyDroppedBy to null
    /// </summary>
    /// <param name="other">The character who leaves the item's collider</param>
    public virtual void OnTriggerExit2D(Collider2D other)
    {
        if (!other.gameObject.TryGetComponent(out Character character) || character != RecentlyDroppedBy) return;
        RecentlyDroppedBy = null;
    }

    /// <summary>
    /// Completely destroys item from the world. Can destroy item even if its in someones inventory.
    /// </summary>
    public virtual void Destroy()
    {
        if (Inventory != null) Inventory.RemoveItem(this);
        Destroy(gameObject);
    }

    /// <summary>
    /// Toggles tooltip.
    /// </summary>
    /// <param name="show">Tooltip will show if set to true</param>
    public virtual void ToggleTooltip(bool show)
    {
        if (show && string.IsNullOrWhiteSpace(Tooltip)) return;

        TooltipController.Instance.Text = Tooltip;
        TooltipController.Instance.IsVisible = show;
    }

    /// <summary>
    /// Show tooltip when hovering mouse over item
    /// </summary>
    public virtual void OnMouseEnter()
    {
        MouseOver = true;
        ToggleTooltip(true);
    }

    /// <summary>
    /// Hide tooltip when not hovering mouse over item
    /// </summary>
    public virtual void OnMouseExit()
    {
        MouseOver = false;
        ToggleTooltip(false);
    }

    /// <summary>
    /// This is called when player click this item while its in the inventory
    /// </summary>
    /// <param name="eventData">Event data</param>
    public virtual void OnClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Inventory.DropItem(this);
            DisplayInventory.Instance.MouseOverItem = false;
        }
    }

    protected virtual void Awake()
    {
    }

    protected virtual void Start()
    {
        if (this is Consumable)
        {
            _icon = GetComponent<SpriteRenderer>().sprite;
        }
    }

    protected virtual void Update()
    {
    }
}