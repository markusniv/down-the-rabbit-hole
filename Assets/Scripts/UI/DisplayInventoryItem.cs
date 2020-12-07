using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Control script for indivitual item in inventory. Handles things like onclick and hover effects.
/// </summary>
public class DisplayInventoryItem : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Item Item { get; set; }

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

    public void OnPointerEnter(PointerEventData eventData)
    {
        Item.OnMouseEnter();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Item.OnMouseExit();
    }
}