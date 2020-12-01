using UnityEngine;

/// <summary>
/// This script manages healthbar in the UI.
/// </summary>

public class Healthbar : MonoBehaviour
{
    /// <summary>
    /// Reference to the health transform
    /// </summary>
    RectTransform Health;

    void Awake()
    {
        Health = transform.Find("Health").GetComponent<RectTransform>();
    }

    void FixedUpdate()
    {
        SyncHealth();
    }

    /// <summary>
    /// Syncs healthbar width to the players health.
    /// </summary>
    void SyncHealth()
    {
        var maxWidth = (transform as RectTransform).sizeDelta.x;
        var per = Mathf.Clamp((float)GameController.Instance.Player.CurrentHealth / (float)GameController.Instance.Player.MaxHealth, 0, 1f);
        Health.sizeDelta = new Vector2(maxWidth * per, 0);
    }

}
