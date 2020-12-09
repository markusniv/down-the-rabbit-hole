using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Controls everything in the main menu. Adds listeners to the buttons and handles loading the game scene
/// with an AsyncOperation and activates the load bar for this.
/// </summary>
public class MenuController : MonoBehaviour
{
    /// <summary>
    /// All the text elements in the menu
    /// </summary>
    [SerializeField] private Text loadingText, progressText;

    /// <summary>
    /// All the buttons in the menu
    /// </summary>
    [SerializeField] private Button start, exit, options, back;

    /// <summary>
    /// The load bar for loading the next scene
    /// </summary>
    [SerializeField] private GameObject loadBar;

    public float load = 0;

    /// <summary>
    /// Adding listeners to each button
    /// </summary>
    private void Start()
    {
        start.onClick.AddListener(() => SoundManagerScript.PlaySound(SoundManagerScript.Sound.Select));
        exit.onClick.AddListener(() => SoundManagerScript.PlaySound(SoundManagerScript.Sound.Select));
        options.onClick.AddListener(() => SoundManagerScript.PlaySound(SoundManagerScript.Sound.Select));
        back.onClick.AddListener(() => SoundManagerScript.PlaySound(SoundManagerScript.Sound.Select));
        start.onClick.AddListener(LoadButton);
        exit.onClick.AddListener(() => Application.Quit());
    }

    /// <summary>
    /// Starts the coroutine for loading the next scene
    /// </summary>
    private void LoadButton()
    {
        loadBar.SetActive(true);
        StartCoroutine(LoadScene());
    }

    /// <summary>
    /// Runs the AsyncOperation for loading the next scene and handles setting the text for the load bar and such
    /// </summary>
    /// <returns></returns>
    private IEnumerator LoadScene()
    {
        yield return null;

        AsyncOperation asyncSceneLoad = SceneManager.LoadSceneAsync("GameScene");
        asyncSceneLoad.allowSceneActivation = false;

        while (!asyncSceneLoad.isDone)
        {
            load = asyncSceneLoad.progress;
            progressText.text = "Loading: " + (asyncSceneLoad.progress * 100) + "%";

            if (asyncSceneLoad.progress >= 0.9f)
            {
                load = 1f;
                progressText.text = "Loading: 100%";
                loadingText.text = "Press space bar to continue.";
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    asyncSceneLoad.allowSceneActivation = true;
                }
            }

            yield return null;
        }
    }
}