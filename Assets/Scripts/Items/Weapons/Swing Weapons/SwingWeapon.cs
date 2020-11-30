using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    /// <summary>
    /// This class includes all swing type weapons. They swing in a 180 degree arc to hit a half a circle in the attack direction
    /// </summary>
    public class SwingWeapon : Weapon
    {
        private float z;
        private Vector3 currentEulerAngles;

        public override void Attack()
        {
            Vector2 attackerPosition = character.transform.position;
            SpriteRenderer sr = GetComponent<SpriteRenderer>();

            // Attacking up requires special calculations so check that first

            sr.enabled = true;

            /*if (attacker.down)
            {
                InitiateAttack(attackerPosition, 0, -1f, 90, 270, attackUp);
            }
            if (attacker.up)
            {

            }*/
        }

        private void InitiateAttack(Vector2 attackerPosition, float xModifier, float yModifier, float startAngle, float endAngle, bool attackUp)
        {

        }
    }
}

