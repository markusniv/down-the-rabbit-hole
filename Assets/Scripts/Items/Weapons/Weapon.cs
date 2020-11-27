using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract base class for all weapons
/// </summary>
namespace Weapons
{
    public abstract class Weapon : Item
    {
        /// <summary>
        /// Base tooltip for all weapons 
        /// </summary>
        public override string Tooltip => string.Format("{0}\n" +
                                                            "Damage: {1}\n" +
                                                            "Attack speed: {2}\n" +
                                                            "Cooldown: {3} seconds", weaponName, damage, rotationSpeed, attackCooldownDefault);
        /// <summary>
        /// Attribute variables for all weapon stats
        /// </summary>
        public float rotationSpeed,
                     stabSpeed,
                     damage,
                     attackCooldownDefault,
                     attackCooldown;

        /// <summary>
        /// The character holding the weapon and their hand which is the parent object of all weapons
        /// </summary>
        public Character character;
        public CharacterCombat CharacterCombat;
        public Transform hand;

        /// <summary>
        /// The pseudorandom weapon type that determines stats of the weapon
        /// </summary>
        private RollWeaponType weaponType;

        public string weaponName;

        private SpriteRenderer sr;
        private TrailRenderer tr;
        private Collider2D col;

        public bool attack,
                    attackStarted;

        /// <summary>
        /// Base weapon sprite and the sprite of the weapon when held  
        /// </summary>
        public Sprite weaponSprite,
                      weaponSpriteHeld;

        // Start is called before the first frame update
        public override void Start()
        {
            base.Start();

            // Get the base components of the weapon. Stab weapons don't have a TrailRenderer so check for that before disabling it for other weapons.

            sr = GetComponent<SpriteRenderer>();
            tr = GetComponent<TrailRenderer>();
            col = GetComponent<Collider2D>();

            if (tr != null)
            {
                tr.enabled = false;
            }

            attack = false;

            // Check if the weapon doesn't have a predetermined type already set

            if (!gameObject.name.Contains("Of"))
            {
                // Change the weapon base stats according to the weapon type
                if (weaponType == null)
                {
                    weaponType = GetComponent<RollWeaponType>();
                    weaponName = gameObject.name;
                    gameObject.name = gameObject.name + weaponType.GetWeaponName(weaponType.random);
                    weaponName += weaponType.weaponName;
                    rotationSpeed = weaponType.rotationSpeed;
                    stabSpeed = weaponType.stabSpeed;
                    damage = weaponType.damage;
                    attackCooldownDefault = weaponType.attackCooldown;
                }

                // If the weapon spawns in a character's inventory, change the sprite and disable sprite and collider
                if (transform.parent != null)
                {
                    sr.sprite = weaponSpriteHeld;
                    Hide();
                }

                // Else the weapon is on the ground, so show basic sprite
                else
                {
                    sr.sprite = weaponSprite;
                    sr.enabled = true;
                }
            }
        }

        /// <summary>
        /// Change SpriteRenderer and TrailRenderer sorting orders and/or flip sprite according to the hit direction
        /// </summary>
        public override void Update()
        {
            if (attack)
            {
                if (character.GetComponent<CharacterAnimation>().up)
                {
                    sr.sortingOrder = 2;
                    tr.sortingOrder = 1;

                }
                else
                {
                    sr.sortingOrder = 4;
                    tr.sortingOrder = 3;
                }
            }
        }

        /// <summary>
        /// Override default OnPickup, changing sprite to the weapon held sprite and reseting their rotation
        /// </summary>
        /// <param name="pickedUpBy">Who picked up this weapon</param>
        public override void OnPickup(Character pickedUpBy)
        {
            base.OnPickup(pickedUpBy);
            sr.sprite = weaponSpriteHeld;
            transform.localRotation = Quaternion.identity;
            Hide();
        }
        /// <summary>
        /// Override default OnDrop, changing sprite to the regular weapon sprite
        /// </summary>
        /// <param name="droppedBy"></param>
        public override void OnDrop(Character droppedBy)
        {
            base.OnDrop(droppedBy);
            sr.sprite = weaponSprite;
            Show();
        }
        /// <summary>
        /// This hides the weapon collider, sprite and trailrenderer
        /// </summary>
        public virtual void Hide()
        {
            col.enabled = false;
            sr.enabled = false;
            if (tr != null)
            {
                tr.enabled = false;
                tr.Clear();
            }
        }
        /// <summary>
        /// This shows the weapon collider, sprite and trailrenderer
        /// </summary>
        public virtual void Show()
        {
            col.enabled = true;
            sr.enabled = true;
            if (tr != null)
            {
                tr.enabled = true;
            }
        }
        protected override void OnMouseEnter()
        {
            if (Inventory != null)
            {
                if (!attack)
                {
                    base.OnMouseEnter();
                }
            }
            else
            {
                base.OnMouseEnter();
            }
        }

        protected override void OnMouseExit()
        {
            if (Inventory != null)
            {
                if (!attack)
                {
                    base.OnMouseExit();
                }
            }
            else
            {
                base.OnMouseExit();
            }

        }
        */

        /// <summary>
        /// Abstract class for the weapon's attack, overridden in the different weapon type classes
        /// </summary>
        public abstract void Attack();

    }
}
