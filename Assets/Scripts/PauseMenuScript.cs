using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public GameObject pauseMenuPanel;

    void Start()
    {
        // Ensure the pause menu is initially hidden
        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(false);
        }
    }

    void Update()
    {
        // Toggle pause menu when the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    public void ResumeGame()
    {
        // Hide the pause menu
        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(false);
            Time.timeScale = 1; // Unpause the game
        }
    }

    public void QuitGame()
    {
        // Quit the game (only works in standalone builds)
        Application.Quit();
    }

    void TogglePauseMenu()
    {
        // Toggle the active state of the pause menu panel
        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(!pauseMenuPanel.activeSelf);

            // Pause/unpause the game Time.timeScale to freeze/unfreeze gameplay
            Time.timeScale = pauseMenuPanel.activeSelf ? 0 : 1;
        }
    }
}
