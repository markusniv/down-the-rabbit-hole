using System;
using System.Collections.Generic;
using UnityEngine;
using Weapons;


/// <summary>
/// This class is used as characters inventory. Each character should have Inventory component.
/// </summary>
public class Inventory : MonoBehaviour
{
    /// <summary>
    /// Private backing field for <see cref="ActiveItem"/>. DO NOT SET DIRECTLY.
    /// </summary>
    private Item _activeItem;

    /// <summary>
    /// Contains all _items in <see cref="Inventory"/>. DO NOT MODIFY DIRECTLY.
    /// </summary>
    public List<Item> _items = new List<Item>();


    /// <summary>
    /// This contains reference to current active item. Public accessor for <see cref="_activeItem"/>
    /// </summary>
    public Item ActiveItem
    {
        get
        {
            return _activeItem;
        }
        set
        {
            if (_activeItem != null) _activeItem.gameObject.SetActive(false);
            Character.Combat.CurrentWeapon = value as Weapon;
            _activeItem = value;
            _activeItem?.gameObject.SetActive(true);
            OnActiveItemChange?.Invoke(); // TODO: Set cursor on invoke
        }
    }

    /// <summary>
    /// Reference to the Character who owns this <see cref="Inventory"/>
    /// </summary>
    public Character Character { get; private set; }

    /// <summary>
    /// This is public accessor for <see cref="_items"/>. Use this to read _items in <see cref="Inventory"/>.
    /// </summary>
    public IReadOnlyList<Item> Items => _items;
    #region Events
    /// <summary>
    /// This event is triggered always when <see cref=""/>
    /// </summary>
    public event Action OnActiveItemChange;

    /// <summary>
    /// This event is triggered always when item is removed or added to inventory
    /// </summary>
    public event Action OnChange;
    #endregion


    /// <summary>
    /// Adds <see cref="Item"/> into <see cref="Items"/> after checking if the player already has 8 useable _items in inventory.
    /// Hotbar limit was set to 8, so no more than 8 consumables and/or weapons can be held at once. The player can however have
    /// unlimited relics.
    /// </summary>
    /// <param name="item">Item to be added</param>
    public void AddItem(Item item)
    {
        if (Character is Player)
        {
            if (item is ICanHotbar)
            {
                var hotbar = GameObject.Find("Hotbar");
                if (hotbar.transform.childCount == 8)
                {
                    return;
                } else
                {
                    CompleteAddItem(item);
                }
            }
            else
            {
                CompleteAddItem(item);
            }
        }
        else
        {
            CompleteAddItem(item);
        }
    }
    /// <summary>
    /// Completes adding the item into inventory.
    /// </summary>
    /// <param name="item">Item being added.</param>
    void CompleteAddItem(Item item)
    {
        // Character who throw this item cannot pick it up mid air
        if (item is Consumable consumable && consumable.ThrownBy == Character) return;

        item.gameObject.transform.SetParent(Character.Combat.Hand);
        item.Inventory = this;
        if (item is Weapon weapon)
        {
            weapon.character = Character;
            weapon.CharacterCombat = Character.Combat;
            weapon.hand = Character.Combat.Hand;
        }
        item.gameObject.transform.localPosition = new Vector3(0, 1, 0);
        item.gameObject.SetActive(false);
        item.SpriteRenderer.enabled = false;
       
        item.OnPickup(Character);
        if (ActiveItem == null) ActiveItem = item;
        _items.Add(item);
        OnChange?.Invoke();
    }

    /// <summary>
    /// Drops <see cref="Item"/> to the ground.
    /// </summary>
    /// <param name="item">Item to be dropped</param>
    public void DropItem(Item item)
    {
        if (item == ActiveItem) ActiveItem = null;
        item.OnDrop(Character);
        item.Inventory = null;
        item.gameObject.SetActive(true);
        item.SpriteRenderer.enabled = true;
        item.RecentlyDroppedBy = Character;
        item.gameObject.transform.SetParent(null);
        item.transform.position = Character.transform.position;
        item.transform.eulerAngles = Vector3.zero;
        _items.Remove(item);
        OnChange?.Invoke();
    }

    /// <summary>
    /// Removes item from inventory. DO NOT USE TO DROP ITEM.
    /// </summary>
    /// <param name="item"></param>
    public void RemoveItem(Item item) {
        if (ActiveItem == item) ActiveItem = null;
        _items.Remove(item);
        OnChange?.Invoke();
    }

    /// <summary>
    /// Get the character who owns this inventory
    /// </summary>
    private void Awake()
    {
        Character = GetComponent<Character>();
    }


    private void FixedUpdate()
    {
        
    }

    /// <summary>
    /// Add item to inventory when colliding with one
    /// </summary>
    /// <param name="other">The item we're colliding with</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        // If this collided with item we can pick up
        if(other.gameObject.TryGetComponent(out Item item) && item.RecentlyDroppedBy != Character && item.Inventory == null)
        {
            // Enemy can pick up consumable ONLY if it was thrown
            if (!(Character is Player) && item is Consumable consumable && !consumable.Thrown) return;
            AddItem(item);
        }
    }
}