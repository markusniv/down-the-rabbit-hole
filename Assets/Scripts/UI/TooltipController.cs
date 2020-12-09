using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script for tooltip that is shown when user hovers items.
/// </summary>
public class TooltipController : MonoBehaviour
{
    private static TooltipController _instance;

    /// <summary>
    /// Gets main instance of <see cref="Tooltip"/>. Simple singleton pattern.
    /// </summary>
    public static TooltipController Instance
    {
        get
        {
            if (_instance == null) _instance = GameObject.FindGameObjectWithTag("Tooltip").GetComponent<TooltipController>();
            return _instance;
        }
    }

    private bool _isVisible;

    /// <summary>
    /// Sets visibility of tooltip.
    /// </summary>
    public bool IsVisible
    {
        get
        {
            return _isVisible;
        }
        set
        {
            Border.SetActive(value);
            _isVisible = value;
        }
    }

    [SerializeField]
    private Text TextComponent;

    [SerializeField]
    private GameObject Background;

    [SerializeField]
    private GameObject Border;

    private string _text;

    /// <summary>
    /// Sets and Gets current text of the tooltip
    /// </summary>
    public string Text
    {
        get
        {
            return _text;
        }
        set
        {
            _text = value;
            TextComponent.text = value;
        }
    }

    private void Start()
    {
        // Hide tooltip if player changes their inventory.
        GameController.Instance.Player.Inventory.OnChange += () =>
        {
            IsVisible = false;
        };
    }

    /// <summary>
    /// Moves tooltip if it is shown
    /// </summary>
    private void Update()
    {
        if (!IsVisible) return;
        transform.position = Input.mousePosition + (transform.position - Background.transform.position) + new Vector3(20, 5);
    }
}