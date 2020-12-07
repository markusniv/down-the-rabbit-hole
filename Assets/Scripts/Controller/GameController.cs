﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject inventory;

    static GameController _instance;
    /// <summary>
    /// Gets main instance of <see cref="GameController"/>. Simple singleton pattern.
    /// </summary>
    public static GameController Instance { get
        {
            if (_instance == null) _instance = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
            return _instance;
        }
    }

    public Floor CurrentFloor;

    public Player Player;

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
            } else
            {
                foreach (Transform child in inventory.transform)
                {
                    child.GetComponent<DisplayInventoryItem>().Item.OnMouseExit();
                }
                inventory.SetActive(false);
            }
        }
        
    }
}
