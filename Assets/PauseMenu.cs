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
            bool isOpening = !pauseMenu.activeSelf;

            pauseMenu.SetActive(isOpening);

            if (!isOpening)
            {
                settingsPanel.SetActive(false);
                achievementsPanel.SetActive(false);
            }

            Time.timeScale = isOpening ? 0 : 1;
        }
    }

    public void ResumeButton()
    {
        pauseMenu.SetActive(false);
        settingsPanel.SetActive(false);
        achievementsPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void MainMenuButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void OpenSettings()
    {
        pauseMenu.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
            settingsPanel.SetActive(false);
            pauseMenu.SetActive(true);
    }

    public void BackButton()
    {
        settingsPanel.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void OpenAchievements()
    {
        achievementsPanel.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void CloseAchievements()
    {
        achievementsPanel.SetActive(false);
        pauseMenu.SetActive(true);
    }
}
