﻿using System;
using System.Collections.Generic;
using UnityEngine;


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
    /// Contains all items in <see cref="Inventory"/>. DO NOT MODIFY DIRECTLY.
    /// </summary>
    private List<Item> _items = new List<Item>();


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
            // TODO: Set CurrentWeapon
            _activeItem = value;
            _activeItem.gameObject.SetActive(true);
            OnActiveItemChange?.Invoke(); // TODO: Set cursor on invoke
        }
    }

    /// <summary>
    /// Reference to the Character who owns this <see cref="Inventory"/>
    /// </summary>
    public Character Character { get; private set; }

    /// <summary>
    /// This is public accessor for <see cref="_items"/>. Use this to read items in <see cref="Inventory"/>.
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
    /// Adds <see cref="Item"/> into <see cref="Items"/>.
    /// </summary>
    /// <param name="item">Item to be added</param>
    public void AddItem(Item item)
    {
        item.Inventory = this;
        // TODO: added weapon specific code to Weapon class OnPickup method
        // TODO: Set Hand as Parent
        item.gameObject.transform.localPosition = new Vector3(0, 1, 0);
        item.gameObject.SetActive(false);
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
        item.Inventory = null;
        item.gameObject.SetActive(false);
        item.gameObject.transform.SetParent(null);
        item.transform.position = Character.transform.position;
        item.OnDrop(Character);
        _items.Remove(item);
        OnChange?.Invoke();
    }

    /// <summary>
    /// Removes item from inventory. DO NOT USE TO DROP ITEM.
    /// </summary>
    /// <param name="item"></param>
    public void RemoveItem(Item item) {
        _items.Remove(item);
        OnChange?.Invoke();
    }


    private void Awake()
    {
        Character = GetComponent<Character>();
    }


    private void FixedUpdate()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        // If this collided with item we can pick up
        if(other.gameObject.TryGetComponent(out Item item) && item.RecentlyDroppedBy != Character)
        {
            AddItem(item);
        }
    }
}