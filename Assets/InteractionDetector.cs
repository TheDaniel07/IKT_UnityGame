using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionDetector : MonoBehaviour
{
    private IInteractable InteractableInRange = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            InteractableInRange?.Interact();
        }

    }
     

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out IInteractable interactable))
        {
            InteractableInRange = interactable;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable) && interactable == InteractableInRange)
        {
            InteractableInRange = null;
        }
    }
}
