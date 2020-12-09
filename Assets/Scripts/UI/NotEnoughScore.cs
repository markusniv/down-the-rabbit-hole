using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Shows up if the player doesn't have enough points to open the chest and tells them how much they need
/// </summary>
public class NotEnoughScore : MonoBehaviour
{
    /// <summary>
    /// Text field of the object, showing what is required to open the chest
    /// </summary>
    private Text notEnoughText;

    /// <summary>
    /// Timer to hide the object
    /// </summary>
    public float timer;

    // Start is called before the first frame update
    private void Start()
    {
        notEnoughText = GetComponent<Text>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (timer >= 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            notEnoughText.text = "";
        }
    }

    public void ShowText(string text)
    {
        notEnoughText.text = text;
    }
}