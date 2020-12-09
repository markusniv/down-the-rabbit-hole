using UnityEngine;
/// <summary>
/// Script that controls chest
/// </summary>
public class Chest : MonoBehaviour
{
    /// <summary>
    /// Will be setting the item in the chest.
    /// </summary>
    public GameObject hiddenItem;

    /// <summary>
    /// The particle effect when the chest is opened
    /// </summary>
    public GameObject particleEffect;

    /// <summary>
    /// Gets both sprite of open chest and close chest.
    /// </summary>
    public Sprite chestOpen, chestClose;

    /// <summary>
    /// Sets both open and close to false.
    /// </summary>
    public bool open, close;

    /// <summary>
    /// Sets both enter and exit to false.
    /// </summary>
    public bool enter, exit;

    /// <summary>
    /// Sets for close true and open false.
    /// </summary>
    private void Start()
    {

        close = true;
        open = false;
        enter = false;
    }

    /// <summary>
    /// Checks if player wants to open the chest. Spawns <see cref="hiddenItem"/> if players opens the chest
    /// </summary>
    private void Update()
    {
        // if press enter will go to next code
        if (enter)
        {
            // KeyDown will check if you press enter or E
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.E))
            {
                // If the chest is close it will continue to next code
                if (close)
                {
                    // Will change the close to false and open to true.
                    close = false;
                    open = true;

                    var chestItem = Instantiate(hiddenItem, transform.position - new Vector3(0, 2f), Quaternion.identity, transform.parent);
                    chestItem.name = hiddenItem.name;
                    chestItem.SetActive(true);
                    chestItem.GetComponent<SpriteRenderer>().sortingOrder = 2;
                    var particle = Instantiate(particleEffect, transform.position, Quaternion.identity);

                    GetComponent<SpriteRenderer>().sprite = chestOpen;
                    SoundManagerScript.PlaySound(SoundManagerScript.Sound.ChestOpen);
                }
            }
        }
    }

    /// <summary>
    /// Checks if player is close to the chest
    /// </summary>
    /// <param name="collision">Collider</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            exit = false;
            enter = true;
        }
    }

    /// <summary>
    /// Checks if player leaves the chest
    /// </summary>
    /// <param name="collision">Collider</param>
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            enter = false;
            exit = true;
        }
    }
}