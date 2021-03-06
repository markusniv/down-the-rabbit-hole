using UnityEngine;

/// <summary>
/// Abstract base class for all weapons
/// </summary>
public abstract class Weapon : Item, ICanHotbar
{
    /// <summary>
    /// Attribute variables for all weapon stats
    /// </summary>
    public float rotationSpeed,
                 stabSpeed,
                 damage,
                 attackCooldownDefault,
                 attackCooldown;

    /// <summary>
    /// The character holding the weapon and their hand which is the Character object of all weapons
    /// </summary>
    public Character character;

    public CharacterCombat CharacterCombat;
    public CharacterMovement characterMovement;
    public Transform hand;
    public float angle;

    /// <summary>
    /// The pseudorandom weapon type that determines stats of the weapon
    /// </summary>
    private RollWeaponType weaponType;

    public string weaponName;
    private TrailRenderer tr;
    private Collider2D col;

    public bool attack,
                attackStarted;

    /// <summary>
    /// Base weapon sprite and the sprite of the weapon when held
    /// </summary>
    public Sprite weaponSprite,
                  weaponSpriteHeld;

    protected override void Awake()
    {
        base.Awake();
        tr = GetComponent<TrailRenderer>();
        col = GetComponent<Collider2D>();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        // Get the base components of the weapon. Stab weapons don't have a TrailRenderer so check for that before disabling it for other weapons.

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
        }
    }

    /// <summary>
    /// Change SpriteRenderer and TrailRenderer sorting orders and/or flip sprite according to the hit direction
    /// </summary>
    protected override void Update()
    {
        if (attack)
        {
        }
    }

    /// <summary>
    /// Override default OnPickup, changing sprite to the weapon held sprite and reseting their rotation
    /// </summary>
    /// <param name="pickedUpBy">Who picked up this weapon</param>
    public override void OnPickup(Character pickedUpBy)
    {
        base.OnPickup(pickedUpBy);
        MouseOver = false;
        transform.localPosition = new Vector2(0, 5);
        characterMovement = pickedUpBy.gameObject.GetComponent<CharacterMovement>();
        SpriteRenderer.sprite = weaponSpriteHeld;
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
        SpriteRenderer.sprite = weaponSprite;
        Show();
    }

    /// <summary>
    /// This hides the weapon collider, sprite and trailrenderer
    /// </summary>
    public virtual void Hide()
    {
        col.enabled = false;
        SpriteRenderer.enabled = false;
        if (tr != null)
        {
            tr.enabled = false;
            tr.Clear();
        }
    }

    /// <summary>
    /// This shows the weapon collider, sprite and trailrenderer
    /// </summary>
    public virtual void Show(bool withoutCollider = false)
    {
        col.enabled = !withoutCollider;
        SpriteRenderer.enabled = true;
        if (tr != null)
        {
            tr.enabled = true;
        }
    }

    /// <summary>
    /// If the weapon is in inventory and it is not attacking, or if
    /// the item is on ground, do base.OnMouseEnter, e.g. show item details
    /// </summary>
    public override void OnMouseEnter()
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

    /// <summary>
    /// If the weapon is in inventory and it is not attacking, or if
    /// the item is on ground, do base.OnMouseExit, e.g. stop showing item details
    /// </summary>
    public override void OnMouseExit()
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

    /// <summary>
    /// Abstract method for the weapon's attack, overridden in the different weapon type classes
    /// </summary>
    public abstract void Attack();

    /// <summary>
    /// This method moves weapon to block position.
    /// </summary>
    public virtual void Block()
    {
        var angle = Vector2.SignedAngle(Vector2.up, Inventory.Character.Movement.LookDirection) + 90;
        hand.localEulerAngles = new Vector3(0, 0, angle);
        hand.localPosition = Inventory.Character.Movement.LookDirection - (Vector2)hand.transform.up;
    }

    /// <summary>
    /// Handle picking up the item and actually hitting characters with weapon if the weapon is in
    /// someone's inventory. Disallow enemies from hitting each other, only allow player to hit enemies
    /// and enemy hit player
    /// </summary>
    /// <param name="collision"></param>
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.TryGetComponent(out Character characterHit) && Inventory != null)
        {
            if (Inventory.Character is Player && characterHit is Enemy)
            {
                OnHit(characterHit);
            }
            else if (Inventory.Character is Enemy && characterHit is Player)
            {
                OnHit(characterHit);
            }
        }
    }

    /// <summary>
    /// Turn characters back to their original color when weapon exits collision after hit
    /// </summary>
    /// <param name="collision"></param>
    public override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
        if (collision.gameObject.TryGetComponent(out Character characterHit) && Inventory != null)
        {
            characterHit.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    /// <summary>
    /// Handle the weapon hits, reducing character healths and turning them red when hit, except
    /// with a successful block
    /// </summary>
    /// <param name="character"></param>
    public virtual void OnHit(Character character)
    {
        if (character.Combat.CurrentState is Blocking block)
        {
            block.OnHit();
            return;
        }
        /// <summary>
        /// If the power gantlent is equip it will increase the value of the hit by the percentage we gave it.
        /// </summary>
        SoundManagerScript.PlaySound(SoundManagerScript.Sound.Onhit);

        character.CurrentHealth -= Inventory.Character.Damage(damage);
        character.GetComponent<SpriteRenderer>().color = Color.red;
    }

    /// <summary>
    /// Primary item use, e.g. left mouse button, start an attack
    /// </summary>
    public void PrimaryUse()
    {
        if (Inventory.Character.Combat.CurrentState is Idle && Inventory.Character.Combat.AttackCooldown == null && !MouseOver)
        {
            Inventory.Character.Combat.CurrentState = new Attacking(Inventory.Character);
        }
    }

    /// <summary>
    /// Secondary item use, e.g. right mouse button, start a block
    /// </summary>
    public void SecondaryUse()
    {
        if (!(Inventory.Character.Combat.CurrentState is Idle) || MouseOver || Inventory.Character.Combat.BlockCooldown != null) return;
        Inventory.Character.Combat.BlockCooldown = 0.2f;
        Inventory.Character.Combat.CurrentState = new Blocking(Inventory.Character);
    }
}