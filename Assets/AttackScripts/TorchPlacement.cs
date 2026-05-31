using UnityEngine;
using UnityEngine.InputSystem;

public class TorchPlacement : MonoBehaviour
{
    public GameObject Torch;
    public Transform parent;
    public Rigidbody2D player;
    public InventoryManager inventory;

    public void PlaceTorch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector3 playerPosition = player.transform.position;
            Instantiate(Torch, playerPosition, Quaternion.identity, parent);
            inventory.RemoveItem("torch", 1);
        }
    }

}
