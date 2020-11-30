using System.Collections;
using System.Collections.Generic;
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
    public Sprite Icon;

    /// <summary>
    /// This is reference to the Character who dropped this item. This is used to prevent item pickup immediately after dropping it.
    /// </summary>
    public Character RecentlyDroppedBy;

    /// <summary>
    /// Items value in some currency
    /// </summary>
    public int Value;

    #region Components
    public SpriteRenderer SpriteRenderer { get; private set; }
    #endregion

    /// <summary>
    /// Cursor that is used when this item is active
    /// </summary>
    // TODO: Finish this implementation
    public virtual object Cursor { get; }

    /// <summary>
    /// Reference to the Inventory where this item is stored. This value is null if item is on the ground.
    /// </summary>
    public Inventory Inventory { get; set; }

    /// <summary>
    /// Tooltip that is shown when mouse is over this item in the UI
    /// </summary>
    public virtual string Tooltip { get; }
    protected virtual void FixedUpdate() { }

    /// <summary>
    /// This is called when this item is dropped from inventory
    /// </summary>
    /// <param name="droppedBy">Who dropped this item</param>
    public virtual void OnDrop(Character droppedBy) { }

    /// <summary>
    /// This is called when this item is added to inventory.
    /// </summary>
    /// <param name="pickedUpBy">Who picked this item</param>
    public virtual void OnPickup(Character pickedUpBy) { }
    public virtual void OnTriggerExit2D(Collider2D other)
    {

        if (RecentlyDroppedBy != other.gameObject) return;
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
        // TODO: Do the tooltip code
    }

    public virtual void OnMouseEnter()
    {
        ToggleTooltip(true);
    }

    public virtual void OnMouseExit()
    {
        ToggleTooltip(false);
    }

    /// <summary>
    /// This is called when player click this item while its in the inventory
    /// </summary>
    /// <param name="eventData">Event data</param>
    public virtual void OnClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            Inventory.DropItem(this);
        }
    }

    protected virtual void Start() { }
    protected virtual void Update() { }
}
