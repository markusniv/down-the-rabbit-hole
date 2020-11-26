using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{

    public override string Tooltip => string.Format("{0}\n" +
                                                        "Damage: {1}\n" +
                                                        "Attack speed: {2}\n" +
                                                        "Cooldown: {3} seconds", weaponName, damage, rotationSpeed, attackCooldownDefault);
    public float rotationSpeed,
                 stabSpeed,
                 damage,
                 attackCooldownDefault,
                 attackCooldown;

    public Character character;
    public CharacterCombat CharacterCombat;
    public Transform hand;

    private RollWeaponType weaponType;

    public string weaponName;

    private SpriteRenderer sr;
    private TrailRenderer tr;
    private Collider2D col;

    public bool attack,
                attackStarted;

    public Sprite weaponSprite,
                  weaponSpriteHeld;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
