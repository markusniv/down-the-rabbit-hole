using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    void Start()
    {
        /// <summary>
        /// Sets for close true and open false.
        /// </summary>
        close = true;
        open = false;
        enter = false;
    }

    private void Update()
    {
        Debug.Log(enter);
        /// <summary>
        /// if press enter will go to next code
        /// </summary>
        if (enter)
        {
            /// <summary>
            /// KeyDown will check if you press enter or E
            /// </summary>
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.E))
            {
                /// <summary>
                /// If the chest is close it will continue to next code
                /// </summary>
                if (close)
                {
                    /// <summary>
                    /// Will change the close to false and open to true.
                    /// </summary>
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            exit = false;
            enter = true;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            enter = false;
            exit = true;
        }
    }

}

