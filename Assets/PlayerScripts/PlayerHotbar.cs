using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHotbar : MonoBehaviour
{
    public string InHand { get; private set; }
    private void Start()
    {
        InHand = "pickaxe";
    }

    public void SwapItems(InputAction.CallbackContext swapItemsKeyPressed)
    {
        if(swapItemsKeyPressed.performed && InHand == "pickaxe")
        {
            InHand = "sword";
        }
        else if (swapItemsKeyPressed.performed && InHand == "sword")
        {
            InHand = "pickaxe";
        }
    }
}
