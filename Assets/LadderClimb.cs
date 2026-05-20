using UnityEngine;

public class LadderClimb : MonoBehaviour, IInteractable
{
    [SerializeField] private AbstractDungeonGenerator dungeonGenerator;
    [SerializeField] private Rigidbody2D player;
    public int Level { get; private set; }
    public void Start()
    {
        Level = 0;
    }
    public void Interact()
    {
        ClimbLadder();
    }

    private void ClimbLadder()
    {
        dungeonGenerator.GenerateDungeon();
        player.transform.position = Vector3.zero;
        Level++;
    }
}
