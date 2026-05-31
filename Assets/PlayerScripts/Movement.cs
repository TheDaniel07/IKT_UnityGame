using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public float speed = 10.0f;
    private Vector2 input;
    private Rigidbody2D rb;
    public GameObject isGamePaused;
    private Animator animator;
    public Transform Aim;
    bool isMoving = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void ResumeButtonPressed()
    {
        PauseController.SetPause(false);
        isGamePaused.SetActive(false);
    }

    void Update()
    {
        if (PauseController.IsGamePaused)
        {
            input = Vector2.zero;
            isGamePaused.SetActive(true);
            return;
        }

        if (!PauseController.IsGamePaused)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if (input.y != 0.0 || input.x != 0.0)
            {
                animator.SetBool("isMoving", true);
                isMoving = true;
                animator.SetFloat("InputX", input.x);
                animator.SetFloat("InputY", input.y);
            }
            else
            {
                isMoving = false;
                animator.SetBool("isMoving", false);
                animator.SetFloat("LastInputX", input.x);
                animator.SetFloat("LastInpuY", input.y);
            }

            input.Normalize();
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = input * speed;
        if (isMoving)
        {
            Vector3 vector3 = Vector3.left * input.x + Vector3.down * input.y;
            Aim.rotation = Quaternion.LookRotation(Vector3.forward ,vector3);
        }
    }
}