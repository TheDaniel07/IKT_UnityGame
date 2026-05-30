using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelCounter : MonoBehaviour
{
    [SerializeField]
    private LadderClimb ladderClimb;
    [SerializeField]
    private TMP_Text counterText, InHandText;
    [SerializeField]
    private PlayerHotbar hotbar;
    void Update()
    {
        InHandText.text = $"In Hand: {hotbar.InHand.ToString()}";
        counterText.text = $"Level: {ladderClimb.Level.ToString()}";
    }
}
