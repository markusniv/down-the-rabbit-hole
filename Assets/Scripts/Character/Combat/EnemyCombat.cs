
using System.Linq;
using UnityEngine;
/// <summary>
/// Script that manages enemy combat. Changes states according to some events
/// </summary>
public class EnemyCombat : CharacterCombat
{
    /// <summary>
    /// Chance of doing defensive action when player attacks. Default is 10%. <see cref="Enemy.DifficultyModifier"/> is applied to this.
    /// </summary>
    DifficultyAware DefensiveActionChance;

    /// <summary>
    /// Enemy's attack delay in seconds. Enemy will not attack even though they could. They will attack after this delay has passed.
    /// </summary>
    DifficultyAware AttackDelay;

    /// <summary>
    /// Timer for attack delay.
    /// </summary>
    float? AttackDelayTimer;

    protected override void Start()
    {
        // Creating Difficulty aware DefensiveActionChance. Chance grows exponentially with DifficultyModifier, but never goes over 50%
        DefensiveActionChance = new DifficultyAware(
            10, // 10% base chance to block
            ((Enemy)Character).DifficultyModifier, // DifficultyModifier
            x => Mathf.Clamp(x.BaseValue * Mathf.Pow(x.DifficultyModifier, 2), 10, 50)
        );

        AttackDelay = new DifficultyAware(
            1f, // Seconds,
            ((Enemy)Character).DifficultyModifier,
            x => x.BaseValue / x.DifficultyModifier // Attack delay will get smaller as the difficulty rises.
            );

        base.Start();
        GameController.Instance.Player.Combat.OnAttackStart += OnPlayerAttacking;

        AddRandomWeaponToInventory();
        AddRandomConsumableItemsToInventory();
    }

    /// <summary>
    /// Adds random potion to the inventory
    /// </summary>
    void AddRandomConsumableItemsToInventory()
    {
        // 50% chance to add potion
        if(Random.Range(0,2) == 0)
        {
            var consumables = PrefabHelper.GetItems<Consumable>();
            var createdConsumable = Instantiate(consumables.ElementAt(Random.Range(0, consumables.Count())));
            Character.Inventory.AddItem(createdConsumable.GetComponent<Item>());
            createdConsumable.transform.localPosition = new Vector3(0, 1, 0);
        }
    }

    /// <summary>
    /// takes random weapon from Prefabs/Items/Weapons folder and adds it to inventory
    /// </summary>
    void AddRandomWeaponToInventory()
    {
        var possibleWeapons = Resources.LoadAll<GameObject>("Prefabs/Items/Weapons");
        var selectedWeapon = possibleWeapons[Random.Range(0, possibleWeapons.Length)];

        var createdWeapon = Instantiate(selectedWeapon);
        createdWeapon.name = createdWeapon.name.Replace("(Clone)", "");
        Character.Inventory.AddItem(createdWeapon.GetComponent<Item>());
        createdWeapon.gameObject.transform.localPosition = new Vector3(0, 1, 0);
    }

    /// <summary>
    /// Triggered when Player attacks. Here enemy will try to block or dodge based on some change.
    /// </summary>
    /// <param name="weapon"></param>
    private void OnPlayerAttacking(Weapon weapon)
    {
        // Can't do anything if state isn't idle
        if (!(CurrentState is Idle)) return;
        // Don't react if not following the player
        if (!(Character.Movement.CurrentState is FollowTarget)) return;

        // Enemy will perform defensive action
        if (Random.Range(0, 101) < DefensiveActionChance.Value)
        {
            // 50%/50% chance to either dodge or block
            if (Random.value < 0.5f)
            {
                Character.Combat.CurrentState = new Blocking(Character);
            }else
            {
                Character.Combat.CurrentState = new Dodging(Character, GameController.Instance.Player);
            }
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        TryAttacking();
    }

    /// <summary>
    /// Enemy tries to attack
    /// </summary>
    private void TryAttacking()
    {
        // Can't attack if state isn't idle
        if (!(CurrentState is Idle)) return;
        // Can't attack if doesn't have a weapon
        if (CurrentWeapon == null) return;
        // Can't attack if attack is still on cooldown
        if (AttackCooldown != null) return;
        // Can't attack if enemy isn't following player
        if (!(Character.Movement.CurrentState is FollowTarget follow)) return;

        // Can't attack if not in range
        if (!follow.AtRange) return;

        // Enemy could attack, but it hasn't waited AttackDelay
        if(AttackDelayTimer == null)
        {
            AttackDelayTimer = AttackDelay.Value;
        }else if(AttackDelayTimer <= 0f) // Enemy has waited attack delay. There we can start the attack
        {
            Character.Combat.CurrentState = new Attacking(Character);
            AttackDelayTimer = null;
        }else
        {
            AttackDelayTimer -= Time.fixedDeltaTime;
        }
    }
}