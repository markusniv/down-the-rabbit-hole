using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages Inventory UI element. Displays players current items.
/// </summary>
public class DisplayInventory : MonoBehaviour
{
    /// <summary>
    /// Indicates if player hovers some item in inventory
    /// </summary>
    public bool MouseOverItem;

    private static DisplayInventory _instance;
    public static DisplayInventory Instance
    {
        get
        {
            if (_instance == null) _instance = GameObject.FindGameObjectWithTag("Inventory").GetComponent<DisplayInventory>();
            return _instance;
        }
    }

    /// <summary>
    /// Whos inventory we want to display
    /// </summary>
    public Character Character;

    /// <summary>
    /// Prefab we use to display items in the grid.
    /// </summary>
    private GameObject ItemPrefab;

    // Start is called before the first frame update
    private void Start()
    {
        ItemPrefab = Resources.Load<GameObject>("Prefabs/UI/UI Inventory Item");
        Character = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Character.Inventory.OnChange += UpdateView;
        _instance = this;
        gameObject.SetActive(false);
    }

    /// <summary>
    /// This method updates the grid. It always removes old items and creates new when inventory changes
    /// </summary>
    private void UpdateView()
    {
        foreach (Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }
        if (Character.Inventory.Items == null) return;
        foreach (var item in Character.Inventory.Items)
        {
            var newItem = Instantiate(ItemPrefab);
            newItem.GetComponent<DisplayInventoryItem>().Item = item;
            newItem.name = item.name;
            newItem.transform.SetParent(gameObject.transform);
            var sprite = item.Icon.GetInstanceID() == 0 ? item.GetComponent<SpriteRenderer>().sprite : item.Icon;
            newItem.GetComponent<Image>().sprite = sprite;
            newItem.GetComponent<Image>().color = item.GetComponent<SpriteRenderer>().color;
        }
    }
}