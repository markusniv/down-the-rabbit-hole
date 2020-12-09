using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Shows floor number after rooms are created
/// </summary>
public class ShowFloorNumber : MonoBehaviour
{
    /// <summary>
    /// Format of the text
    /// </summary>
    private string Format = "Floor {0}";

    private Text Text;
    private Animator Animator;

    private void Awake()
    {
        Text = GetComponent<Text>();
        Animator = GetComponent<Animator>();
        GameController.Instance.CurrentFloor.OnFloorCreated += Show;
    }

    private void Start()
    {
    }

    /// <summary>
    /// Start show animation and change the text
    /// </summary>
    /// <param name="floor"></param>
    private void Show(int floor)
    {
        Text.text = string.Format(Format, floor);
        Animator.SetTrigger("Show");
    }
}