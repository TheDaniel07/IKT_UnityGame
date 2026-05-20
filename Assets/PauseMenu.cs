using System.ComponentModel;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject settingsPanel;
    public GameObject achievementsPanel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HandleEscape();
        }
    }

    void HandleEscape()
    {
        if (settingsPanel.activeSelf)
        {
            CloseSettings();
            return;
        }

        if (achievementsPanel.activeSelf)
        {
            CloseAchievements();
            return;
        }

        if (pauseMenu.activeSelf)
        {
            ResumeButton();
            return;
        }
    }

    public void ResumeButton()
    {
        pauseMenu.SetActive(false);
        settingsPanel.SetActive(false);
        achievementsPanel.SetActive(false);
        PauseController.SetPause(false);
    }

    public void OpenSettings()
    {
        pauseMenu.SetActive(false);
        settingsPanel.SetActive(true);
        achievementsPanel.SetActive(false);
    }

    public void OpenAchievements()
    {
        pauseMenu.SetActive(false);
        achievementsPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    void CloseSettings()
    {
        settingsPanel.SetActive(false);
        pauseMenu.SetActive(true);
    }

    void CloseAchievements()
    {
        achievementsPanel.SetActive(false);
        pauseMenu.SetActive(true);
    }
}