using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject inventory = null;

    private static GameController _instance;

    /// <summary>
    /// Gets main instance of <see cref="GameController"/>. Simple singleton pattern.
    /// </summary>
    public static GameController Instance
    {
        get
        {
            if (_instance == null) _instance = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
            return _instance;
        }
    }

    public Floor CurrentFloor;

    public Player Player;

    public GameObject Compass;
    private void Awake()
    {
        CurrentFloor = GameObject.Find("Floor").GetComponent<Floor>();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventory.activeSelf == false)
            {
                inventory.SetActive(true);
            }
            else
            {
                foreach (Transform child in inventory.transform)
                {
                    if (child.GetComponent<DisplayInventoryItem>().Item.MouseOver == true)
                    {
                        child.GetComponent<DisplayInventoryItem>().Item.OnMouseExit();
                    }
                }
                DisplayInventory.Instance.MouseOverItem = false;
                inventory.SetActive(false);
            }
        }
        Compass.SetActive(Input.GetKey(KeyCode.C));
    }
}