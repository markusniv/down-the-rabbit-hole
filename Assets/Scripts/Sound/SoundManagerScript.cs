using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Basic sound manager to store all sound effects and to call them from here whenever needed
/// </summary>
public class SoundManagerScript : MonoBehaviour
{

    static Dictionary<Sound, AudioClip> Sounds;
    // Enum that stores each sound effect
    public enum Sound
    {
        QuickSlash,
        SlowSlash,
        QuickThrust,
        Deflect,
        Select,
        Pickup,
        ChestOpen,
        Thrown,
        Onhit
    }

    static AudioSource audioSrc;
    /// <summary>
    /// Store each sound effect into a dictionary
    /// </summary>
    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
        audioSrc = GetComponent<AudioSource>();
        Sounds = new Dictionary<Sound, AudioClip> {
            {Sound.QuickSlash, Resources.Load<AudioClip>("Sounds/Items/Weapon/quickslash") },
            {Sound.Deflect, Resources.Load<AudioClip>("Sounds/Items/Weapon/deflect") },
            {Sound.Select, Resources.Load<AudioClip>("Sounds/UI/select") },
            {Sound.ChestOpen, Resources.Load<AudioClip>("Sounds/Items/Consumable/ChestOpen") },
            {Sound.Pickup, Resources.Load<AudioClip>("Sounds/Items/Consumable/PickUpItem") },
            {Sound.Thrown, Resources.Load<AudioClip>("Sounds/Items/Consumable/Thrown") },
            {Sound.Onhit, Resources.Load<AudioClip>("Sounds/Items/Weapon/onhit") },
            {Sound.QuickThrust, Resources.Load<AudioClip>("Sounds/Items/Weapon/QuickThrust") }




        };
    }
    /// <summary>
    /// Play the called sound effect
    /// </summary>
    /// <param name="sound">Called sound effect</param>
    public static void PlaySound(Sound sound)
    {
        if (Sounds.TryGetValue(sound, out AudioClip clip))
        {
            audioSrc.PlayOneShot(clip);
        }
    }
}
