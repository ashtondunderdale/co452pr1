using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public int speed = 2;
    private Rigidbody2D characterBody;
    private Vector2 velocity;
    private Vector2 inputMovement;

    void Start()
    {
        velocity = new Vector2 (speed, speed);
        characterBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        inputMovement = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );
    }

    private void FixedUpdate()
    {
        Vector2 delta = inputMovement * velocity * Time.deltaTime;
        Vector2 newPosition = characterBody.position + delta;
        characterBody.MovePosition(newPosition);
    }
}