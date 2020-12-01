using UnityEngine;
using UnityEngine.UI;
using Weapons;

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

    /// <summary>
    /// Enables <see cref="CooldownText"/> Component.
    /// </summary>
    /// <param name="weapon">Weapon that is currently on cooldown</param>
    void Show(Weapon weapon)
    {
        CooldownText.enabled = true;
    }

    /// <summary>
    /// Disables <see cref="CooldownText"/> Component.
    /// </summary>
    /// <param name="weapon">Weapon that is currently on cooldown</param>
    void Hide(Weapon weapon)
    {
        CooldownText.enabled = false;
    }

    void FixedUpdate()
    {
        if (GameController.Instance.Player.Combat.AttackCooldown <= 0) return;
        UpdateCooldownText();
    }

    /// <summary>
    /// Updates cooldown text.
    /// </summary>
    void UpdateCooldownText()
    {
        CooldownText.text = string.Format("Cooldown: {0}", Mathf.Round(GameController.Instance.Player.Combat.AttackCooldown));
    }
}