using System;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Shows cooldown text when player attacks
/// </summary>
public class ShowCooldown : MonoBehaviour
{
    /// <summary>
    /// Reference to the component that is used to show cooldown.
    /// </summary>
    private Text CooldownText;

    void Awake()
    {
        CooldownText = GetComponent<Text>();

    }

    void Start()
    {
        GameController.Instance.Player.Combat.OnCooldownStart += Show;
        GameController.Instance.Player.Combat.OnCooldownEnd += Hide;
    }

    /// <summary>
    /// Enables <see cref="CooldownText"/> Component.
    /// </summary>
    /// <param name="weapon">Weapon that is currently on cooldown</param>
    void Show()
    {
        CooldownText.enabled = true;
    }

    /// <summary>
    /// Disables <see cref="CooldownText"/> Component.
    /// </summary>
    /// <param name="weapon">Weapon that is currently on cooldown</param>
    void Hide()
    {
        CooldownText.enabled = false;
    }

    /// <summary>
    /// Updates cooldown text if attack is on cooldown
    /// </summary>
    void FixedUpdate()
    {
        if (GameController.Instance.Player.Combat.AttackCooldown == null) return;
        UpdateCooldownText();
    }

    /// <summary>
    /// Updates cooldown text.
    /// </summary>
    void UpdateCooldownText()
    {
        CooldownText.text = string.Format("Cooldown: {0}", Math.Round(GameController.Instance.Player.Combat.AttackCooldown ?? 0f, 2));
    }
}