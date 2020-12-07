using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneHandler : MonoBehaviour
{
    [SerializeField] GameObject playerObject,
                                deathText,
                                pressAnyKeyText,
                                hotBar,
                                healthBar;

    private Player player;

    private State playerState;
    // Start is called before the first frame update
    void Start()
    {
        player = playerObject.GetComponent<Player>();
        playerState = player.GetComponent<CharacterCombat>().CurrentState;
    }

    // Update is called once per frame
    void Update()
    {
        playerState = player.GetComponent<CharacterCombat>().CurrentState;
        Debug.Log(playerState);
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
                SceneManager.LoadScene("Main Menu");
            }
        }
    }
}
