using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    /// <summary>
    /// This class includes all swing type weapons. They swing in a 180 degree arc to hit a half a circle in the attack direction.
    /// Down: 90' -> 270'
    /// Left: 0' -> 180'
    /// Right: 180' -> 360'
    /// Up: 270' -> 0' -> 90' (Rotates over the 360' mark, is the only one that requires special code)
    /// </summary>
    public class SwingWeapon : Weapon
    {
        private float z = 1;
        private Vector3 currentEulerAngles;

        /// <summary>
        /// The main attack function, initiates attacks into the various directions by giving the InitiateAttack
        /// function specific variables depending on the direction
        /// </summary>
        public override void Attack()
        {
            Animate attacker = characterAnimation;
            SpriteRenderer sr = GetComponent<SpriteRenderer>();

            sr.enabled = true;
            
            #region Initiate attacks
            if (attacker.down) InitiateAttack(0,
                                              -1,
                                              90,
                                              0,
                                              270,
                                              false);
            if (attacker.up) InitiateAttack(0,
                                            0.5f,
                                            270,
                                            260,
                                            90,
                                            true);
            if (attacker.left) InitiateAttack(-1,
                                               0,
                                               0,
                                               0,
                                               180,
                                               false);
            if (attacker.right) InitiateAttack(1,
                                               0,
                                               180,
                                               0,
                                               355,
                                               false);
            #endregion
        }
        /// <summary>
        /// Initiates the actual attack. Rotates the hand in a 180 degree arc to allow for a half circle swipe
        /// with the weapon. All directions except up use the same code, as up needs to check when the 
        /// rotation resets back to zero and then start checking if it has reached 90 degrees yet
        /// </summary>
        /// <param name="attackerPosition">Position of the attacking character</param>
        /// <param name="xModifier">Move the hand this much in X direction</param>
        /// <param name="yModifier">Move the hand this much in Y direction</param>
        /// <param name="startAngle">The starting angle of the attack</param>
        /// <param name="middleAngle">The middle angle of the attack (only used by the up attack, else set to 0)</param>
        /// <param name="endAngle">The end angle of the attack</param>
        /// <param name="attackUp">Boolean used to check if attacking up</param>
        private void InitiateAttack(float xModifier, 
                                    float yModifier, 
                                    float startAngle,
                                    float middleAngle,
                                    float endAngle, 
                                    bool attackUp)
        {
            Vector2 attackerPosition = character.transform.position;
            // Set the starting position for the hand
            hand.position = new Vector2(attackerPosition.x + xModifier, attackerPosition.y + yModifier);

            // Executed on the first frame of the attack

            if (!attackStarted)
            {
                // Set weapon rotation to the corresponding starting attack angle
                hand.eulerAngles = new Vector3(0f, 0f, startAngle);
                currentEulerAngles = transform.eulerAngles;

                // Don't allow this to be run more than once so set attackStarted to true;

                attackStarted = true;
            }

            if (!attackUp)
            {
                // Once the attack has been started, check if we have reached the full 180 degrees of attack.
                // If not, keep rotating weapon

                if (hand.eulerAngles.z < endAngle)
                {
                    currentEulerAngles += new Vector3(0, 0, z) * Time.deltaTime * rotationSpeed;
                    hand.eulerAngles = currentEulerAngles;
                }
                // Else, start the cooldown
                else
                {
                    InitiateCooldown();
                }
            }
            else
            {
                if (hand.eulerAngles.z > 0)
                {
                    if (hand.eulerAngles.z > middleAngle)
                    {
                        currentEulerAngles += new Vector3(0, 0, z) * Time.deltaTime * rotationSpeed;
                        hand.eulerAngles = currentEulerAngles;
                    }
                    else if (transform.eulerAngles.z <= endAngle)
                    {
                        currentEulerAngles += new Vector3(0, 0, z) * Time.deltaTime * rotationSpeed;
                        hand.eulerAngles = currentEulerAngles;
                    }
                    else
                    {
                        InitiateCooldown();
                    }
                }
            }
        }
        /// <summary>
        /// Initiates the cooldown for the weapon at the end of the attack
        /// </summary>
        private void InitiateCooldown()
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

