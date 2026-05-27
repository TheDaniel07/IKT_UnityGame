using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    public GameObject Melee;
    bool isAttacking = false;
    float duration = 0.3f;
    float Timer = 0f;

    private void Start()
    {
        Melee.SetActive(false);
    }
    public void CheckInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            CheckMeleeTimer();
            OnAttack();
        }
    }

    private void OnAttack()
    {
        if (!isAttacking)
        {
            Melee.SetActive(true);
            isAttacking = true;
            //Animation playback
        }
    }

    private void CheckMeleeTimer()
    {
        if(Timer >= duration)
        {
            Timer = 0f;
            isAttacking = false;
            Melee.SetActive(false);
        }
    }
}
