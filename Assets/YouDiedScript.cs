using UnityEngine;

public class YouDiedScript : MonoBehaviour
{
    public GameObject exitgameButton;

    public void ExitGameButton()
    {
        Application.Quit();
    }
}
