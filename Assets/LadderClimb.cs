using UnityEngine;

public class LadderClimb : MonoBehaviour, IInteractable
{
    [SerializeField] private AbstractDungeonGenerator dungeonGenerator;
    [SerializeField] private Rigidbody2D player;
    public int Level { get; private set; }
    public void Awake()
    {
        FadeInGameStart();
        Level = 1;
    }
    public void Interact()
    {
        ClimbLadder();
    }

    private void ClimbLadder()
    {
        FadeTransition();
        Level = Level+1;
    }

    async void FadeTransition()
    {
        await ScreenFadeTransition.Instance.FadeOut();
        dungeonGenerator.GenerateDungeon();
        player.transform.position = Vector3.zero;
        await ScreenFadeTransition.Instance.FadeIn();
    }

    async void FadeInGameStart()
    {
        await ScreenFadeTransition.Instance.FadeIn();
    }
}
