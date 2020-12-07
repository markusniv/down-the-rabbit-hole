using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    /// <summary>
    /// This class includes all stab type weapons. They thrust straight infront of the character in a straight line
    /// </summary>
    public class StabWeapon : Weapon
    {
        /// <summary>
        /// Tooltip for stab weapons
        /// </summary>
        public override string Tooltip => string.Format("{0}\n" +
                                                            "Damage: {1}\n" +
                                                            "Stab speed: {2}\n" +
                                                            "Cooldown: {3} seconds", weaponName, damage, stabSpeed, attackCooldownDefault);
        /// <summary>
        /// Variables for the length of the stab aswell as a check to see if we've reached the edge of the stab
        /// </summary>
        private float stabLength = 1f;
        private bool reachedEdge = false;

        /// <summary>
        /// This function performs the stab attack. On the first frame it checks for attackStarted to see if 
        /// the first frame of the attack has gone off. On the first frame, the character's hand position
        /// is set to the correct position and rotation depending on the direction of the attack. We then
        /// start thrusting towards that direction at the stab speed of the equipped weapon.
        /// 
        /// Once the attack reaches the wanted length, the weapon starts coming back towards the character,
        /// until it is back at the starting position, at which point the attack is finished.
        /// </summary>
        /// 

        public override void Attack()
        {
            Animate attacker = characterAnimation;
            Vector2 attackerPosition = character.transform.position;
            SpriteRenderer sr = GetComponent<SpriteRenderer>();

            sr.enabled = true;

            if (attacker.up)
            {
                sr.sortingOrder = 2;
            }
            else
            {
                sr.sortingOrder = 4;
            }

            if (attacker.down)
            {

                // Executed on the first frame of the attack

                if (!attackStarted)
                {
                    SoundManagerScript.PlaySound(SoundManagerScript.Sound.QuickThrust);
                    // Set weapon rotation to the corresponding attack angle
                    hand.position = new Vector2(attackerPosition.x, attackerPosition.y);
                    hand.eulerAngles = new Vector3(0f, 0f, 180);

                    // Don't allow this to be run more than once so set attackStarted to true;
                    reachedEdge = false;
                    attackStarted = true;
                }

                // Once the attack has been started, check if we have reached the length of the attack
                // If not, keep thrusting weapon forward

                if (hand.localPosition.y > -stabLength && !reachedEdge)
                {
                    hand.localPosition = new Vector3(hand.localPosition.x, hand.localPosition.y - stabSpeed, hand.localPosition.z);
                }
                if (hand.localPosition.y <= -stabLength)
                {
                    reachedEdge = true;
                }
                if (reachedEdge && hand.transform.position.y <= attackerPosition.y)
                {
                    hand.localPosition = new Vector3(hand.localPosition.x, hand.localPosition.y + stabSpeed, hand.localPosition.z);
                }
                else if (reachedEdge && hand.transform.position.y >= attackerPosition.y)
                {
                    // Start cooldown for attack
                    attackCooldown = attackCooldownDefault;
                    // Once we've reached the end of the attack, stop attacking
                    attack = false;
                    attackStarted = false;
                    CharacterCombat.CurrentState = new Idle(character);
                    CharacterCombat.AttackCooldown = attackCooldownDefault;
                }
            }
            if (attacker.up)
            {
                // Executed on the first frame of the attack

                if (!attackStarted)
                {
                    // Set weapon rotation to the corresponding attack angle
                    hand.position = new Vector2(attackerPosition.x, attackerPosition.y - 0.5f);
                    hand.eulerAngles = new Vector3(0f, 0f, 0f);
                    reachedEdge = false;
                    // Don't allow this to be run more than once so set attackStarted to true;

                    attackStarted = true;
                }

                // Once the attack has been started, check if we have reached the length of the attack
                // If not, keep thrusting weapon forward

                if (hand.localPosition.y < stabLength - 0.5f && !reachedEdge)
                {
                    hand.localPosition = new Vector3(hand.localPosition.x, hand.localPosition.y + stabSpeed, hand.localPosition.z);
                }
                if (hand.localPosition.y >= stabLength - 0.5f)
                {
                    reachedEdge = true;
                }
                if (reachedEdge && hand.transform.position.y >= attackerPosition.y)
                {
                    hand.localPosition = new Vector3(hand.localPosition.x, hand.localPosition.y - stabSpeed, hand.localPosition.z);
                }
                else if (reachedEdge && hand.transform.position.y <= attackerPosition.y)
                {
                    // Start cooldown for attack
                    attackCooldown = attackCooldownDefault;
                    // Once we've reached the end of the attack, stop attacking
                    attack = false;
                    attackStarted = false;
                    CharacterCombat.CurrentState = new Idle(character);
                    CharacterCombat.AttackCooldown = attackCooldownDefault;
                }
            }
            if (attacker.right)
            {
                // Executed on the first frame of the attack

                if (!attackStarted)
                {
                    // Set weapon rotation to the corresponding attack angle
                    hand.position = new Vector2(attackerPosition.x, attackerPosition.y - 0.2f);
                    hand.eulerAngles = new Vector3(0f, 0f, -90f);

                    // Don't allow this to be run more than once so set attackStarted to true;
                    reachedEdge = false;
                    attackStarted = true;
                }

                // Once the attack has been started, check if we have reached the length of the attack
                // If not, keep thrusting weapon forward

                if (hand.localPosition.x < stabLength && !reachedEdge)
                {
                    hand.localPosition = new Vector3(hand.localPosition.x + stabSpeed, hand.localPosition.y, hand.localPosition.z);
                }
                if (hand.localPosition.x >= stabLength)
                {
                    reachedEdge = true;
                }
                if (reachedEdge && hand.transform.position.x >= attackerPosition.x)
                {
                    hand.localPosition = new Vector3(hand.localPosition.x - stabSpeed, hand.localPosition.y, hand.localPosition.z);
                }
                else if (reachedEdge && hand.transform.position.x <= attackerPosition.x)
                {
                    // Start cooldown for attack
                    attackCooldown = attackCooldownDefault;
                    // Once we've reached the end of the attack, stop attacking
                    attack = false;
                    attackStarted = false;
                    CharacterCombat.CurrentState = new Idle(character);
                    CharacterCombat.AttackCooldown = attackCooldownDefault;
                }
            }
            if (attacker.left)
            {
                // Executed on the first frame of the attack

                if (!attackStarted)
                {
                    // Set weapon rotation to the corresponding attack angle
                    hand.position = new Vector2(attackerPosition.x, attackerPosition.y - 0.2f);
                    hand.eulerAngles = new Vector3(0f, 0f, 90f);

                    // Don't allow this to be run more than once so set attackStarted to true;
                    reachedEdge = false;
                    attackStarted = true;
                }

                // Once the attack has been started, check if we have reached the length of the attack
                // If not, keep thrusting weapon forward

                if (hand.localPosition.x > -stabLength && !reachedEdge)
                {
                    hand.localPosition = new Vector3(hand.localPosition.x - stabSpeed, hand.localPosition.y, hand.localPosition.z);
                }
                if (hand.localPosition.x <= -stabLength)
                {
                    reachedEdge = true;
                }
                if (reachedEdge && hand.transform.position.x <= attackerPosition.x)
                {
                    hand.localPosition = new Vector3(hand.localPosition.x + stabSpeed, hand.localPosition.y, hand.localPosition.z);
                }
                else if (reachedEdge && hand.transform.position.x >= attackerPosition.x)
                {
                    // Start cooldown for attack
                    attackCooldown = attackCooldownDefault;
                    // Once we've reached the end of the attack, stop attacking
                    attack = false;
                    attackStarted = false;
                    CharacterCombat.CurrentState = new Idle(character);
                    CharacterCombat.AttackCooldown = attackCooldownDefault;
                }
            }


        }
    }
    
}

