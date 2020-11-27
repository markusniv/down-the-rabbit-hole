using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This item controls indivitual items in hotbar.
/// </summary>
public class HotbarItem : MonoBehaviour
{

    public ICanHotbar Item;

    #region Components
    public Text HotkeyText;
    public Image Icon;
    #endregion

    public int Hotkey;

    public void Awake()
    {
        HotkeyText = GetComponentInChildren<Text>();
        Icon = GetComponent<Image>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        HotkeyText.text = Hotkey.ToString();
    }

}