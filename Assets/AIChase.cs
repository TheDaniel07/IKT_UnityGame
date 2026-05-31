using UnityEngine;

public class AIChase : MonoBehaviour
{
    [SerializeField]private GameObject player;
    public float speed;
    public float distanceBetween;
    [SerializeField]private GameObject PauseMenu;

    private float distance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    private void FixedUpdate()
    {
        PauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
    }

    // Update is called once per frame
    void Update()
    {
        
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();

        if (distance < distanceBetween)
        {
            Vector3 LookDirection = player.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle-90);
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        }

        if (PauseMenu != null && PauseMenu.activeSelf)
        {
            speed = 0;
        }
        else
        {
            speed = 3;
            return;
        }

    }
}
