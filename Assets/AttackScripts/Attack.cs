using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    public GameObject Melee;
    public float attackDamage = 10f;
    bool isAttacking = false;
    float duration = 0.3f;
    float timer = 0f;

    private void Start()
    {
        Melee.SetActive(false);
    }

    private void Update()
    {
        CheckMeleeTimer();
        CheckInput();
    }
    /*
    public void CheckInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnAttack();
        }
    }
    */

    public void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
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
        if (isAttacking)
        {
            timer += Time.deltaTime;
            if(timer >= duration)
            {
                timer = 0;
                isAttacking = false;
                Melee.SetActive(false);
            }
        }
    }
}
