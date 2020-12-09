using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script manages showing player score.
/// </summary>
public class ShowScore : MonoBehaviour
{
    private string Format = "Score: {0}";

    private Text TextComponent;

    private void Awake()
    {
        TextComponent = GetComponent<Text>();
    }

    /// <summary>
    /// Adds listener, so this will update the score when player kills
    /// </summary>
    private void Start()
    {
        GameController.Instance.Player.OnScoreChange += UpdateScore;
    }

    /// <summary>
    /// Updates Score when <see cref="Player.Score"/> Changes
    /// </summary>
    /// <param name="newScore">New score to be shown</param>
    private void UpdateScore(float newScore)
    {
        TextComponent.text = string.Format(Format, Mathf.Round(newScore));
    }
}