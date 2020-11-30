using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Control script for indivitual item in inventory. Handles things like onclick and hover effects.
/// </summary>
public class DisplayInventoryItem : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Item Item { get; set; }

    public void OnPointerClick(PointerEventData eventData)
    {
        Item.OnClick(eventData);
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