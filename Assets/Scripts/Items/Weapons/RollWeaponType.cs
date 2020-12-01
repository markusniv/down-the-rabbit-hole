using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

/// <summary>
/// This class takes a weapon prefab and rolls a random type for it, giving it a new name and stats
/// </summary>
public class RollWeaponType : MonoBehaviour
{
    /// <summary>
    /// Attribute variables for all weapon stats
    /// </summary>
    public float rotationSpeed,
                 stabSpeed,
                 damage,
                 attackCooldown;

    public string weaponName;

    /// <summary>
    /// This enum has all the different weapon types stored into it
    /// </summary>
    public enum WeaponType
    {
        OfTheCheetah,
        OfTheBear,
        OfTheRabbit,
        OfTheSloth
    }

    public int random;

    /// <summary>
    /// This function gets the name of the weapon type from the enum
    /// </summary>
    /// <param name="WeaponIndex">The pseudorandom roll for the weapon type</param>
    /// <returns>The name of the weapon type as a string</returns>
    public string GetWeaponName(int WeaponIndex)
    {
        WeaponType WeaponName = (WeaponType)WeaponIndex;
        return WeaponName.ToString();
    }

    /// <summary>
    /// Actually roll the random weapon types, do it as an Awake to do it before any of the other weapon scripts
    /// as not to cause any glitches
    /// </summary>
    private void Awake()
    {
        // If the weapon type is already given (to allow the creation of premade special weapons), do not roll a 
        // weapon type.
        if (!gameObject.name.Contains("Of"))
        {
            // Pick randomly from all the listed types in the enum
            random = Random.Range(0, System.Enum.GetNames(typeof(WeaponType)).Length);

            // Assign a weapon a random bonus type and give its bonuses and/or negatives to the weapon

            rotationSpeed = GetComponent<Weapon>().rotationSpeed;
            stabSpeed = GetComponent<Weapon>().stabSpeed;
            attackCooldown = GetComponent<Weapon>().attackCooldownDefault;
            damage = GetComponent<Weapon>().damage;

            switch (random)
            {
                case 0:
                    weaponName = " of the Cheetah";
                    rotationSpeed *= 2;
                    stabSpeed *= 2;
                    attackCooldown *= 0.8f;
                    break;
                case 1:
                    weaponName = " of the Bear";
                    rotationSpeed *= 0.7f;
                    stabSpeed *= 0.7f;
                    damage *= 3;
                    attackCooldown *= 1.5f;
                    break;
                case 2:
                    weaponName = " of the Rabbit";
                    rotationSpeed *= 2.5f;
                    stabSpeed *= 2.5f;
                    damage *= 0.5f;
                    attackCooldown *= 0.5f;
                    break;
                case 3:
                    weaponName = " of the Sloth";
                    rotationSpeed *= 3f;
                    stabSpeed *= 3f;
                    damage *= 1f;
                    attackCooldown *= 3f;
                    break;
            }
        }
    }
}
