using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionDetector : MonoBehaviour
{
    private IInteractable InteractableInRange = null;
    public GameObject Aim;
    public GameObject Melee;

    private void Update()
    {
        isAimOn();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            InteractableInRange?.Interact();
        }

    }
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

    private void isAimOn()
    {
        if (!Aim.activeSelf)
        {
            Aim.SetActive(true);
            Melee.SetActive(false);
        }
        else if (Aim.activeSelf)
        {
            return;
        }
    }
}
