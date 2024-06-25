using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenuPanel;  // Reference to the pause menu panel
    private bool isPaused = false;
    [SerializeField] Slider musicSlider, sfxSlider;

    void Start()
    {
        // Ensure the pause menu is not visible at the start
        pauseMenuPanel.SetActive(false);

        // Initialize slider values

    }

    void Update()
    {
        // Toggle pause menu on Escape key press
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;  // Pause the game
        isPaused = true;
        ShowCursor();
    }

    public void ResumeGame()
    {
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;  // Resume the game
        isPaused = false;
        HideCursor();
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;  // Ensure the game is not paused
        SceneManager.LoadScene("Menu");  // Load the main menu scene
        SoundManager.instance.musicSource.Stop();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ToggleMusic()
    {
        SoundManager.instance.ToggleMusic();
    }

    public void ToggleSFX()
    {
        SoundManager.instance.ToggleSFX();
    }

    public void MusicVolume()
    {
        SoundManager.instance.MusicVolume(musicSlider.value);
    }

    public void SFXVolume()
    {
        SoundManager.instance.SFXVolume(sfxSlider.value);
    }

    private void ShowCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
