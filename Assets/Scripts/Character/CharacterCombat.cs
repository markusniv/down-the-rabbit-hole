using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombat : MonoBehaviour
{
    /// <summary>
    /// Weapon currently held. Will be null if character doesn't hold weapon.
    /// </summary>
    // TODO: Add Weapon Type
    public object CurrentWeapon { get; set; }

    #region Components
    /// <summary>
    /// Reference to the main Character script
    /// </summary>
    public Character Character;

    /// <summary>
    /// Reference to the hand that should be inside the character
    /// </summary>
    public Transform Hand;

    public Animator Animator;
    #endregion
    protected virtual void Awake()
    {
        Character = GetComponent<Character>();
        Animator = GetComponent<Animator>();
        Hand = Character.gameObject.transform.Find("Hand");
    }

    protected virtual void Start()
    {
    }

    protected virtual void FixedUpdate()
    {
    }

    protected virtual void Update()
    {
    }
}
