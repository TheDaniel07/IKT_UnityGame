using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    public GameObject Pickaxe, Sword;
    public PlayerHotbar hotbar;
    bool isAttacking = false;
    float duration = 0.3f;
    float timer = 0f;

    private void Start()
    {
        Pickaxe.SetActive(false);
        Sword.SetActive(false);
    }

    private void Update()
    {
        CheckMeleeTimer();
    }

    public void CheckInput(InputAction.CallbackContext attack)
    {
        if (attack.performed)
        {
            OnAttack();
        }
    }


    private void OnAttack()
    {
        if (!isAttacking && hotbar.InHand == "pickaxe")
        {
            Pickaxe.SetActive(true);
            isAttacking = true;
            //Animation playback
        }
        if (!isAttacking && hotbar.InHand == "sword")
        {
            Sword.SetActive(true);
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
                Pickaxe.SetActive(false);
                Sword.SetActive(false);
            }
        }
    }
}
