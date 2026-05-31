using UnityEngine;

public class SettingsTabs : MonoBehaviour
{
    public GameObject displaySettings;
    public GameObject audioSettings;
    public GameObject gameSettings;

    private void Start() => ShowTab(0); // Display-t mutatja alapból

    public void ShowTab(int index)
    {
        displaySettings.SetActive(index == 0);
        audioSettings.SetActive(index == 1);
        gameSettings.SetActive(index == 2);
    }
}