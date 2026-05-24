using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public float speed = 10.0f;
    private Vector2 input;
    private Rigidbody2D rb;
    public GameObject isGamePaused;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
            input.Normalize();
            return;
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = input * speed;
    }
}