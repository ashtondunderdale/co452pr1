using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    public float collisionOffset = 0.05f;

    public ContactFilter2D movementFilter;

    public attack attack;

    Vector2 movementInput;

    public Rigidbody2D rb;

    Animator animator;
    SpriteRenderer spriteRenderer;

    List<RaycastHit2D> castCollisions = new();

    bool canMove = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();    
    }

    private void FixedUpdate()
    {
        if (movementInput != Vector2.zero && canMove)
        {
            bool success = TryMove(movementInput);

            if (!success)
            {
                success = TryMove(new Vector2(movementInput.x, 0));

                if (!success && movementInput.x > 0)
                {
                    success = TryMove(new Vector2(0, movementInput.y));
                }

                if (!success && movementInput.y > 0)
                {
                    success = TryMove(new Vector2(0, movementInput.y));
                }
            }
            animator.SetBool("isMoving", success);
        }
        else animator.SetBool("isMoving", false);
            
        if (movementInput.x < 0)
        {
            spriteRenderer.flipX = true;
            attack.attackDirection = attack.AttackDirection.left;
        }
        else if (movementInput.x > 0) 
        {
            spriteRenderer.flipX = false;
            attack.attackDirection = attack.AttackDirection.right;
        }
    }

    private void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    private bool TryMove(Vector2 direction) 
    {
        if (direction != Vector2.zero)
        {
            int count = rb.Cast(direction, movementFilter, castCollisions, moveSpeed * Time.fixedDeltaTime + collisionOffset);

            if (count == 0)
            {
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            }
            else return false;
        }
        else return false;
    }

    void OnFire() => animator.SetTrigger("attack");

    public void AttackFunction() 
    {
        LockMovement();

        if (spriteRenderer.flipX == true) attack.AttackLeft();
        else attack.AttackRight();
    }

    public void EndAttack() 
    { 
        UnlockMovement();
        attack.StopAttack();
    }

    public void LockMovement() => canMove = false;

    public void UnlockMovement() => canMove = true;
    
}
