using JetBrains.Annotations;
using System;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float multiplierForSprintSpeed;
    private float multiplierForSpeed;
    private Rigidbody2D characterBody;
    private Vector2 velocity;
    private Vector2 inputMovement;

    void Start()
    {
        velocity = new Vector2(speed, speed);
        characterBody = GetComponent<Rigidbody2D>();
        multiplierForSpeed = 10f / speed;
        multiplierForSprintSpeed = 8f / speed;
    }

    void Update()
    {
        inputMovement = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );

        Vector2 delta = (inputMovement * Time.deltaTime).normalized;

        if (Input.GetKey(KeyCode.LeftShift))
            delta /= multiplierForSprintSpeed;
        else
            delta /= multiplierForSpeed;

        Vector2 newPosition = characterBody.position + delta;
        characterBody.MovePosition(newPosition);
    }
}
