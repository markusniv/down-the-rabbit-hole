using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Item : MonoBehaviour
{
    public int Value;

    [SerializeField] private Sprite _icon = null;
    public Sprite Icon => _icon;

    #region Components
    public SpriteRenderer SpriteRenderer { get; set; }
    #endregion

    public virtual string Tooltip { get; }

    public Inventory Inventory { get; set; }

    private GameObject RecentlyDroppedBy;

    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
