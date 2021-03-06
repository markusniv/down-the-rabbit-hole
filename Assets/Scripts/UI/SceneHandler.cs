using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles varying states of the game to control moving between scenes, for example getting back to main menu after player death.
/// </summary>
public class SceneHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject playerObject = null,
                                deathText = null,
                                pausedText = null,
                                pressAnyKeyText = null,
                                pressKeyPause = null,
                                hotBar = null,
                                healthBar = null;

    private Player player;

    private State playerState;

    private bool paused;

    private void Start()
    {
        player = playerObject.GetComponent<Player>();
        playerState = player.Combat.CurrentState;
        paused = false;
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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                hotBar.SetActive(false);
                healthBar.SetActive(false);
                pausedText.SetActive(true);
                pressKeyPause.SetActive(true);
                Time.timeScale = 0;

                paused = true;
                ToggleAllScripts(false);
            }
            else
            {
                hotBar.SetActive(true);
                healthBar.SetActive(true);
                pausedText.SetActive(false);
                pressKeyPause.SetActive(false);
                Time.timeScale = 1;
                ToggleAllScripts(true);
                paused = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (paused)
            {
                ToggleAllScripts(true);
                Time.timeScale = 1;
                Destroy(GameObject.Find("SoundManager"));
                Destroy(GameObject.Find("MusicManager"));
                SceneManager.LoadScene("Main Menu");
            }
        }
    }

    void ToggleAllScripts(bool enabled)
    {
        var allscripts = GameObject.FindObjectsOfType<MonoBehaviour>().Where(x => !(x is SceneHandler) && x.GetType().Assembly == this.GetType().Assembly);
        foreach (var script in allscripts)
        {
            script.enabled = enabled;
        }
    }
}