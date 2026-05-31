using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class TorchPlacement : MonoBehaviour
{
    public GameObject Torch;
    public Transform parent;
    public Rigidbody2D player;
    public InventoryManager inventory;
    public PlayerHealth playerHealth;
    public void PlaceTorch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (inventory.RemoveItem("torch", 1))
            {
                Vector3 playerPosition = player.transform.position;
                Instantiate(Torch, playerPosition, Quaternion.identity, parent);
            }

        }
    }

    public void DrinkHealPotion(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (inventory.RemoveItem("health_potion", 1))
            {
                playerHealth.Heal(10);
            }
        }

    }

}

