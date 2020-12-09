using UnityEngine;

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
        OfTheSloth,
        OfTheTiger,
        OfTheSnail,
        OfTheLion,
        OfTheSoldier,
        OfThePeasant
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
                    rotationSpeed *= 1.2f;
                    stabSpeed *= 1.2f;
                    attackCooldown *= 0.8f;
                    break;

                case 1:
                    weaponName = " of the Bear";
                    rotationSpeed *= 1.8f;
                    stabSpeed *= 1.7f;
                    damage *= 1.6f;
                    attackCooldown *= 1.5f;
                    break;

                case 2:
                    weaponName = " of the Rabbit";
                    rotationSpeed *= 2.6f;
                    stabSpeed *= 2.6f;
                    damage *= 0.5f;
                    attackCooldown *= 0.5f;
                    break;

                case 3:
                    weaponName = " of the Sloth";
                    rotationSpeed *= 2f;
                    stabSpeed *= 3f;
                    damage *= 1f;
                    attackCooldown *= 3f;
                    break;
                case 4:
                    weaponName = " of the Tiger";
                    rotationSpeed *= 1.3f;
                    stabSpeed *= 1.3f;
                    damage *= 1.3f;
                    attackCooldown *= 0.9f;
                    break;
                case 5:
                    weaponName = " of the Snail";
                    rotationSpeed *= 0.6f;
                    stabSpeed *= 0.6f;
                    damage *= 0.8f;
                    attackCooldown *= 3f;
                    break;
                case 6:
                    weaponName = " of the Lion";
                    rotationSpeed *= 1.4f;
                    stabSpeed *= 1.4f;
                    damage *= 1.5f;
                    attackCooldown *= 1.0f;
                    break;
                case 7:
                    weaponName = " of the Soldier";
                    rotationSpeed *= 1f;
                    stabSpeed *= 1;
                    damage *= 1.8f;
                    attackCooldown *= 1.6f;
                    break;
                case 8:
                    weaponName = " of the Peasant";
                    rotationSpeed *= 1.5f;
                    stabSpeed *= 1.5f;
                    damage *= 0.8f;
                    attackCooldown *= 1.6f;
                    break;
            }
        }
    }
}