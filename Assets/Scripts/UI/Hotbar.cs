using System;
using System.Linq;
using UnityEngine;


/// <summary>
/// This script controls the hotbat UI element.
/// </summary>
public class Hotbar : MonoBehaviour
{
    Player Player;
    GameObject ItemPrefab;

    HotbarItem[] ItemsInHotbar = new HotbarItem[8];
    HotbarItem Active;
    RectTransform RectTransform;
    RectTransform CanvasRectTransform;
    CanvasGroup CanvasGroup;

    float MinAlpha = 0.2f;

    float MaxAlphaOnDistanceOver = 200f;


    private void Start()
    {
        ItemPrefab = Resources.Load<GameObject>("Prefabs/UI/HotbarItem");
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Player.Inventory.OnChange += OnInventoryChange;
        RectTransform = transform as RectTransform;
        CanvasRectTransform = GameObject.Find("UI").GetComponent<RectTransform>();
        CanvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        CheckActiveItemChange();
    }

    /// <summary>
    /// When Player inventory changes.
    /// </summary>
    void OnInventoryChange()
    {
        var items = Player.Inventory.Items.OfType<ICanHotbar>().Take(ItemsInHotbar.Length).ToArray();
        RecreateHotbar(items);
    }

    /// <summary>
    /// Recreates the hotbar with new items.
    /// </summary>
    /// <param name="items"></param>
    void RecreateHotbar(ICanHotbar[] items)
    {
        ItemsInHotbar = new HotbarItem[ItemsInHotbar.Length];
        foreach(Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < items.Length; i++)
        {
            ItemsInHotbar[i] = Instantiate(ItemPrefab, transform).GetComponent<HotbarItem>();
            ItemsInHotbar[i].Hotkey = i + 1;
            ItemsInHotbar[i].Item = items[i];
            ItemsInHotbar[i].Icon.sprite = items[i].Icon == null ? items[i].SpriteRenderer.sprite : items[i].Icon;
            ItemsInHotbar[i].Icon.color = new Color(1, 1, 1, 0.2f);
            if (Player.Inventory.ActiveItem == (Item)items[i])
            {
                SetActiveItem(ItemsInHotbar[i]);
            }
            
        }
    }

    /// <summary>
    /// Checks if player tries to change active item
    /// </summary>
    void CheckActiveItemChange()
    {
        if (!(Player.Combat.CurrentState is Idle) || ItemsInHotbar.Count(x => x != null) < 1) return;

        for (int i = 0; i < ItemsInHotbar.Count(x => x != null); i++)
        {
            if(Input.GetKeyDown(ItemsInHotbar[i].Hotkey.ToString()))
            {
                SetActiveItem(ItemsInHotbar[i]);
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            var index = Array.IndexOf(ItemsInHotbar, Active);
            var prevItem = ItemsInHotbar.ElementAtOrDefault(index - 1);
            SetActiveItem(prevItem == null ? ItemsInHotbar.Last(x => x != null) : prevItem);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            var index = Array.IndexOf(ItemsInHotbar, Active);
            var nextItem = ItemsInHotbar.ElementAtOrDefault(index + 1);
            SetActiveItem(nextItem == null ? ItemsInHotbar.First() : nextItem);
        }
    }

    /// <summary>
    /// Sets active item
    /// </summary>
    /// <param name="item">Item that will be set as active</param>
    void SetActiveItem(HotbarItem item)
    {
        if(Active != null)
        {
            Active.Icon.color = new Color(1, 1, 1, 0.2f);
        }
        Active = item;
        item.Icon.color = new Color(1, 1, 1, 1);
        if(item.Item is Item _item)
        {
            Player.Inventory.ActiveItem = _item;
        }
    }

    /// <summary>
    /// Gets distance between hotbar and player by getting hotbar bounds and using <see cref="Bounds.ClosestPoint(Vector3)"/> to get correct distance.
    /// </summary>
    /// <returns>Distance in units</returns>
    float DistanceBetweenHotbarAndPlayer()
    {
        var screenPosition = Camera.main.WorldToScreenPoint(Player.transform.position);
        Vector3[] v = new Vector3[4];
        RectTransform.GetWorldCorners(v);
        var worldCornersRelativeToCam = v.Select(x => Camera.main.ScreenToWorldPoint(x)).ToArray();
        var minDistanceTo = worldCornersRelativeToCam.Min(x => Vector2.Distance(x, Player.transform.position));
        RectTransformUtility.ScreenPointToLocalPointInRectangle(CanvasRectTransform, screenPosition, null, out Vector2 localpoint);
        var bounds = RectTransformUtility.CalculateRelativeRectTransformBounds(CanvasRectTransform, RectTransform);

        var closestPoint = bounds.ClosestPoint(localpoint);
        return Vector3.Distance(closestPoint, localpoint);
    }
    /// <summary>
    /// Fades hotbar to almost transparent when player is close.
    /// </summary>
    void FadeDependingOnPlayerDistance()
    {
        var distance = DistanceBetweenHotbarAndPlayer();
        var fadeAmount = Mathf.Clamp(distance / MaxAlphaOnDistanceOver, MinAlpha, 1f);
        CanvasGroup.alpha = fadeAmount;
    }

    private void FixedUpdate()
    {
        FadeDependingOnPlayerDistance();
    }
}