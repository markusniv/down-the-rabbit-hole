using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A load bar for loading screens
/// </summary>
public class LoadBar : MonoBehaviour
{
    /// <summary>
    /// The RectTransform of the loading bar to manipulate
    /// </summary>
    private RectTransform loading;

    /// <summary>
    /// The controller object in the menu that gives the needed information to manipulate the load bar
    /// </summary>
    [SerializeField] private MenuController menu;

    // Start is called before the first frame update
    void Start()
    {
        var load = GetComponentsInChildren<RectTransform>();
        loading = load[1];
    }
    /// <summary>
    /// Resize the loading bar according to the loading information from the MenuController
    /// </summary>
    private void FixedUpdate()
    {
        var MaxWidth = (transform as RectTransform).sizeDelta.x;
        var per = (float)menu.load / 1f;
        loading.sizeDelta = new Vector2(MaxWidth * per, loading.sizeDelta.y);
        
    }
}
