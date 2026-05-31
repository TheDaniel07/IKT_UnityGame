using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject playButton;
    public GameObject settingsPanel;
    public GameObject exitgameButton;
    public GameObject controlsPanel;
    public GameObject controlsPanel2;

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

        if (controlsPanel.activeSelf && !controlsPanel2.activeSelf)
        {
            controlsPanel.SetActive(false);
            return;
        }

        if (controlsPanel2.activeSelf && controlsPanel.activeSelf)
        {
            controlsPanel2.SetActive(false);
            controlsPanel.SetActive(true);
            return;
        }

        
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("Dungeon");
    }

    public void SettingsBackButton()
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

    public void ControlsButton()
    {
        controlsPanel.SetActive(true);
        controlsPanel2.SetActive(false);
        return;
    }

    public void ControlsBackButton()
    {
        if (controlsPanel.activeSelf && controlsPanel2.activeSelf)
        {
            controlsPanel.SetActive(true);
            controlsPanel2.SetActive(false);
            return;
        }

        if (controlsPanel.activeSelf)
        {
            controlsPanel.SetActive(false);
            return;
        }
    }

    public void ControlsNextButton()
    {
        controlsPanel2.SetActive(true);
        return;
    }

    public void ExitGameButton()
    {
        Application.Quit();
    }
}
