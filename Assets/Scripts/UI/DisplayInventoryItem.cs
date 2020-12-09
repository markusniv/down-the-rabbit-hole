using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Control script for indivitual item in inventory. Handles things like onclick and hover effects.
/// </summary>
public class DisplayInventoryItem : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Item Item { get; set; }

    /// <summary>
    /// Drops or uses item on click
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (Item is Weapon)
        {
            var weapon = Item.gameObject.GetComponent<Weapon>();
            var cooldown = GameObject.Find("Cooldown").GetComponent<Text>();
            if (weapon.attackStarted == false && !cooldown.IsActive())
            {
                Item.OnClick(eventData);
            }
        } else
        {
            Item.OnClick(eventData);
        }
    }

    /// <summary>
    /// Sets mouse over booleans
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        DisplayInventory.Instance.MouseOverItem = true;
        Item.OnMouseEnter();
    }

    /// <summary>
    /// Resets Mouse over booleans
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        DisplayInventory.Instance.MouseOverItem = false;
        Item.OnMouseExit();
    }
}