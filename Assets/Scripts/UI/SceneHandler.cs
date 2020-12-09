﻿using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles varying states of the game to control moving between scenes, for example getting back to main menu after player death.
/// </summary>
public class SceneHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject playerObject = null,
                                deathText = null,
                                pressAnyKeyText = null,
                                hotBar = null,
                                healthBar = null;

    private Player player;

    private State playerState;

    private void Start()
    {
        player = playerObject.GetComponent<Player>();
        playerState = player.Combat.CurrentState;
    }

    /// <summary>
    /// Follow player state, and if player dies, freeze time, hide UI elements and show death text + allow player to get back to main menu
    /// </summary>
    private void Update()
    {
        playerState = player.Combat.CurrentState;
        if (playerState is Dead)
        {
            playerObject.SetActive(false);
            hotBar.SetActive(false);
            healthBar.SetActive(false);
            deathText.SetActive(true);
            pressAnyKeyText.SetActive(true);
            Time.timeScale = 0;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Time.timeScale = 1;
                Destroy(GameObject.Find("SoundManager"));
                Destroy(GameObject.Find("MusicManager"));
                SceneManager.LoadScene("Main Menu");
            }
        }
    }
}