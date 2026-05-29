using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject playButton;
    public GameObject settingsPanel;
    public GameObject exitgameButton;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HandleEscapeMainMenu();
        }
    }

    void HandleEscapeMainMenu()
    {
        if (settingsPanel.activeSelf)
        {
            settingsPanel.SetActive(false);
            return;
        }
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("EnemyAIScene");
    }

    public void SettingsButton()
    {
        if (settingsPanel.activeSelf)
        {
            settingsPanel.SetActive(false);
        }
        else
        {
            settingsPanel.SetActive(true);
        }
    }

    public void ExitGameButton()
    {
        Application.Quit();
    }
}
