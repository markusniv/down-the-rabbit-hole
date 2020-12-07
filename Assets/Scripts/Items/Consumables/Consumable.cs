using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Abtract consumable item. Consumable items can be used only few times. Character can use them directly or throw them.
/// </summary>
public abstract class Consumable : Item, ICanHotbar
{
    /// <summary>
    /// How many times this item can be used until its destroyed.
    /// </summary>
    public int Uses = 1;

    bool Thrown = false;

    Vector2 ThrownFrom;
    Vector2 ThrownTo;
    Character ThrownBy;

    /// <summary>
    /// Consumes item. Item will be destroyed if <see cref="Uses"/> is 0.
    /// </summary>
    public virtual void Consume()
    {
        Uses--;
        if (Uses < 1) Destroy();
    }

    /// <summary>
    /// Consumes item if player used left click on this item.
    /// </summary>
    /// <param name="eventData">Event data of click</param>
    public override void OnClick(PointerEventData eventData)
    {
        base.OnClick(eventData);
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            Consume();
        }
    }

    /// <summary>
    /// This defines primary use. This will be called when player uses left click while this item is <see cref="Inventory.ActiveItem"/>.
    /// </summary>
    public void PrimaryUse()
    {
        Consume();
    }

    /// <summary>
    /// This defines secondary use. this will be called when player uses right click while this item is <see cref="Inventory.ActiveItem"/>.
    /// </summary>
    public void SecondaryUse()
    {
        if (!MouseOver)
        {
            Thrown = true;
            ThrownBy = Inventory.Character;
            ThrownFrom = Inventory.Character.transform.position;
            ThrownTo = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Inventory.DropItem(this);
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        var distance = Vector2.Distance(gameObject.transform.position, ThrownTo);
        if(distance < 1f)
        {
            Destroy(this.gameObject);
        }
    }

    protected override void Update()
    {
        base.Update();
        if(!Thrown) return;
        var direction = (ThrownTo - ThrownFrom).normalized;
        transform.Rotate(new Vector3(0, 0, 1000f * Time.deltaTime));

        transform.position += (Vector3)direction * Time.deltaTime * 5f;

    }

    public override void OnPickup(Character pickedUpBy)
    {
        if (Thrown && ThrownBy == pickedUpBy) return;
        base.OnPickup(pickedUpBy);
        if (Thrown)
        {
            Consume();
        }
    }
}
